using Functions;
using Functions.AutoFunctions;
using System;
using System.Collections.Generic;

namespace MapObject
{
	// Token: 0x020000CF RID: 207
	public class MainObject : IActionListener
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x000A4B64 File Offset: 0x000A2D64
		public static void Update()
		{
			bool flag = MainObject.IsWaiting() || ObjectData.Instance().IsLoading;
			if (!flag)
			{
				bool isWaitNextMap = MainObject.IsWaitNextMap;
				if (isWaitNextMap)
				{
					MainObject.Wait(100);
					MainObject.IsWaitNextMap = false;
				}
				else
				{
					bool isNextMapFailed = MainObject.IsNextMapFailed;
					if (isNextMapFailed)
					{
						ObjectData.Instance().MyLinkMaps = null;
						MainObject.WayXmap = null;
						MainObject.IsNextMapFailed = false;
					}
					else
					{
						bool flag2 = MainObject.WayXmap == null;
						if (flag2)
						{
							bool flag3 = ObjectData.Instance().MyLinkMaps == null;
							if (flag3)
							{
								ObjectData.Instance().LoadLinkMaps();
								return;
							}
							MainObject.WayXmap = MapAlgorithm.FindWay(TileMap.mapID, MainObject.IdMapEnd);
							MainObject.IndexWay = 0;
							bool flag4 = MainObject.WayXmap == null;
							if (flag4)
							{
								GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
								MainObject.FinishXmap();
								return;
							}
						}
						bool flag5 = TileMap.mapID == MainObject.WayXmap[MainObject.WayXmap.Count - 1] && !ObjectData.IsMyCharDie();
						if (flag5)
						{
							GameScr.info1.addInfo("|5|Đã đến: " + MainObject.get_map_names(TileMap.mapID), 0);
							
							MainObject.FinishXmap();
						}
						else
						{
							bool flag6 = TileMap.mapID == MainObject.WayXmap[MainObject.IndexWay];
							if (flag6)
							{
								bool flag7 = ObjectData.IsMyCharDie();
								if (flag7)
								{
									Service.gI().returnTownFromDead();
									MainObject.IsWaitNextMap = (MainObject.IsNextMapFailed = true);
								}
								else
								{
									bool flag8 = ObjectData.CanNextMap();
									if (flag8)
									{
										MainObject.NextMap(MainObject.WayXmap[MainObject.IndexWay + 1]);
										MainObject.IsWaitNextMap = true;
									}
								}
								MainObject.Wait(500);
							}
							else
							{
								bool flag9 = TileMap.mapID == MainObject.WayXmap[MainObject.IndexWay + 1];
								if (flag9)
								{
									MainObject.IndexWay++;
								}
								else
								{
									MainObject.IsNextMapFailed = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000A4D54 File Offset: 0x000A2F54
		public void perform(int idAction, object p)
		{
			bool flag = idAction == 1;
			if (flag)
			{
				MainObject.ShowPanelXmap((List<int>)p);
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00006668 File Offset: 0x00004868
		private static void Wait(int time)
		{
			MainObject.IsWait = true;
			MainObject.TimeStartWait = mSystem.currentTimeMillis();
			MainObject.TimeWait = (long)time;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000A4D78 File Offset: 0x000A2F78
		private static bool IsWaiting()
		{
			bool flag = MainObject.IsWait && mSystem.currentTimeMillis() - MainObject.TimeStartWait >= MainObject.TimeWait;
			if (flag)
			{
				MainObject.IsWait = false;
			}
			return MainObject.IsWait;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000A4DBC File Offset: 0x000A2FBC
		public static void ShowXmapMenu()
		{
			ObjectData.Instance().LoadGroupMapsFromFile();
			if (ObjectData.Instance().GroupMaps.Count == 0)
			{
				GameScr.info1.addInfo("Empty List ", 0);
			}
			else
			{
				MyVector myVector = new MyVector();
				foreach (ObjectGroups groupMap in ObjectData.Instance().GroupMaps)
				{
					myVector.addElement(new Command(groupMap.NameGroup, MainObject._Instance, 1, groupMap.IdMaps));
				}
				GameCanvas.menu.startAt(myVector, 3);
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000A4E54 File Offset: 0x000A3054
		public static string get_map_names(int id)
		{
			//string result = TileMap.mapName;
			//bool flag = id >= 0 && id <= MapData.MapNames.Length && MapData.MapNames[id] != null && MapData.MapNames[id] != string.Empty;
			//if (flag)
			//{
			//	result = MapData.MapNames[id];
			//}
			return TileMap.mapNames[id];
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000A4EA8 File Offset: 0x000A30A8
		public static void ShowPanelXmap(List<int> idMaps)
		{
			MapService.IsMapTransAsXmap = true;
			int count = idMaps.Count;
			GameCanvas.panel.mapNames = new string[count];
			GameCanvas.panel.planetNames = new string[count];
			for (int i = 0; i < count; i++)
            {
                try
                {
                    string str = MainObject.get_map_names(idMaps[i]);
                    GameCanvas.panel.mapNames[i] = idMaps[i].ToString() + ": " + str;
                    GameCanvas.panel.planetNames[i] = "X-MAP";
                }
                catch(Exception e)
                {
					GameScr.info1.addInfo(e.ToString(), 0);
                }
            }
            GameCanvas.panel.setTypeMapTrans();
			GameCanvas.panel.show();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00006682 File Offset: 0x00004882
		public static void StartRunToMapId(int idMap)
		{
			MainObject.IdMapEnd = idMap;
			MapService.IsXmapRunning = true;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00006691 File Offset: 0x00004891
		public static void FinishXmap()
		{
			MapService.IsXmapRunning = false;
			MainObject.IsNextMapFailed = false;
			ObjectData.Instance().MyLinkMaps = null;
			MainObject.WayXmap = null;
			GameCanvas.panel.hide();
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000066BC File Offset: 0x000048BC
		public static void SaveIdMapCapsuleReturn()
		{
			MapService.IdMapCapsuleReturn = TileMap.mapID;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000A4F54 File Offset: 0x000A3154
		private static void NextMap(int idMapNext)
		{
			MainObject.VuDangMapNext = idMapNext;
			List<NextMapObject> mapNexts = ObjectData.Instance().GetMapNexts(TileMap.mapID);
			bool flag = mapNexts != null;
			if (flag)
			{
				foreach (NextMapObject mapNext in mapNexts)
				{
					bool flag2 = mapNext.MapID == idMapNext;
					if (flag2)
					{
						MainObject.NextMap(mapNext);
						return;
					}
				}
			}
			GameScr.info1.addInfo("Lỗi tại dữ liệu", 0);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x000A4FEC File Offset: 0x000A31EC
		private static void NextMap(NextMapObject mapNext)
		{
			switch (mapNext.Type)
			{
			case MapEnum.AutoWaypoint:
				MainObject.NextMapAutoWaypoint(mapNext);
				break;
			case MapEnum.NpcMenu:
				MainObject.NextMapNpcMenu(mapNext);
				break;
			case MapEnum.NpcPanel:
				MainObject.NextMapNpcPanel(mapNext);
				break;
			case MapEnum.Position:
				MainObject.NextMapPosition(mapNext);
				break;
			case MapEnum.Capsule:
				MainObject.NextMapCapsule(mapNext);
				break;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000A504C File Offset: 0x000A324C
		public static int TeleMore(int x)
		{
			bool flag = x < TileMap.pxw - TileMap.pxw + 20;
			int result;
			if (flag)
			{
				result = 100;
			}
			else
			{
				result = -100;
			}
			return result;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000A5080 File Offset: 0x000A3280
		private static void NextMapAutoWaypoint(NextMapObject mapNext)
		{
			Waypoint waypoint = ObjectData.FindWaypoint(mapNext.MapID);
			bool flag = waypoint != null;
			if (flag)
			{
				int posWaypointX = ObjectData.GetPosWaypointX(waypoint);
				int posWaypointY = ObjectData.GetPosWaypointY(waypoint);
				MainObject.MoveMyChar(posWaypointX, posWaypointY);
				global::Char.myCharz().currentMovePoint = new MovePoint(posWaypointX, posWaypointY);
				MainObject.RequestChangeMap(waypoint);
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000A50D4 File Offset: 0x000A32D4
		private static void NextMapNpcMenu(NextMapObject mapNext)
		{
			int num = mapNext.Info[0];
			bool flag = GameScr.findNPCInMap((short)num) == null;
			if (flag)
			{
				MainObject.fixtl();
			}
			else
			{
				MapService.GotoNpc(num);
				Service.gI().openMenu(num);
				for (int i = 0; i < GameCanvas.menu.menuItems.size(); i++)
				{
					bool flag2 = ((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("tương lai") && MainObject.VuDangMapNext >= 92 && MainObject.VuDangMapNext <= 103;
					if (flag2)
					{
						Service.gI().confirmMenu((short)num, (sbyte)i);
						return;
					}
					bool flag3 = ((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("yardart") && MainObject.VuDangMapNext >= 131 && MainObject.VuDangMapNext <= 133;
					if (flag3)
					{
						Service.gI().confirmMenu((short)num, (sbyte)i);
						return;
					}
					bool flag4 = MainObject.VuDangMapNext >= 161 && MainObject.VuDangMapNext <= 164;
					if (flag4)
					{
						bool flag5 = ((Command)GameCanvas.menu.menuItems.elementAt(i)).caption.Trim().ToLower().Contains("thực vật");
						if (flag5)
						{
							Service.gI().confirmMenu((short)num, (sbyte)i);
						}
						else
						{
							Service.gI().useItem(0, 1, (sbyte)AutoUpgradeFunctions.FindIndexItem(992), -1);
						}
						return;
					}
				}
				for (int j = 1; j < mapNext.Info.Length; j++)
				{
					int num2 = mapNext.Info[j];
					Service.gI().confirmMenu((short)num, (sbyte)num2);
				}
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000A52CC File Offset: 0x000A34CC
		private static void fixtl()
		{
			bool flag = TileMap.mapID == 27;
			if (flag)
			{
				MainObject.NextMap(28);
				MainObject.IsWaitNextMap = true;
				MainObject.step = 0;
			}
			else
			{
				bool flag2 = TileMap.mapID == 29;
				if (flag2)
				{
					MainObject.NextMap(28);
					MainObject.IsWaitNextMap = true;
					MainObject.step = 1;
				}
				else
				{
					bool flag3 = MainObject.step == 0;
					if (flag3)
					{
						MainObject.NextMap(29);
						MainObject.IsWaitNextMap = true;
					}
					else
					{
						bool flag4 = MainObject.step == 1;
						if (flag4)
						{
							MainObject.NextMap(27);
							MainObject.IsWaitNextMap = true;
						}
					}
				}
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000A535C File Offset: 0x000A355C
		private static void NextMapNpcPanel(NextMapObject mapNext)
		{
			int num = mapNext.Info[0];
			int num2 = mapNext.Info[1];
			int selected = mapNext.Info[2];
			MapService.GotoNpc(num);
			Service.gI().openMenu(num);
			Service.gI().confirmMenu((short)num, (sbyte)num2);
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000A53B4 File Offset: 0x000A35B4
		private static void NextMapPosition(NextMapObject mapNext)
		{
			int x = mapNext.Info[0];
			int y = mapNext.Info[1];
			MainObject.MoveMyChar(x, y);
			Service.gI().requestChangeMap();
			Service.gI().getMapOffline();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000A53F4 File Offset: 0x000A35F4
		private static void NextMapCapsule(NextMapObject mapNext)
		{
			MainObject.SaveIdMapCapsuleReturn();
			int selected = mapNext.Info[0];
			Service.gI().requestMapSelect(selected);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000066C9 File Offset: 0x000048C9
		public static void UseCapsuleNormal()
		{
			MapService.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)AutoUpgradeFunctions.FindIndexItem(193), -1);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000066EB File Offset: 0x000048EB
		public static void UseCapsuleVip()
		{
			MapService.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, (sbyte)AutoUpgradeFunctions.FindIndexItem(194), -1);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000670D File Offset: 0x0000490D
		public static void HideInfoDlg()
		{
			InfoDlg.hide();
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00006716 File Offset: 0x00004916
		private static void MoveMyChar(int x, int y)
		{
			MapService.GotoSite(x, y);
			Service.gI().requestChangeMap();
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000A5420 File Offset: 0x000A3620
		private static void RequestChangeMap(Waypoint waypoint)
		{
			waypoint.popup.command.performAction();
			bool isOffline = waypoint.isOffline;
			if (isOffline)
			{
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}
		}

		// Token: 0x0400134C RID: 4940
		public static int VuDangMapNext;

		// Token: 0x0400134D RID: 4941
		private static int step;

		// Token: 0x0400134E RID: 4942
		private const int TIME_DELAY_NEXTMAP = 200;

		// Token: 0x0400134F RID: 4943
		private const int TIME_DELAY_RENEXTMAP = 500;

		// Token: 0x04001350 RID: 4944
		private const int ID_ITEM_CAPSULE_VIP = 194;

		// Token: 0x04001351 RID: 4945
		private const int ID_ITEM_CAPSULE = 193;

		// Token: 0x04001352 RID: 4946
		private const int ID_ICON_ITEM_TDLT = 4387;

		// Token: 0x04001353 RID: 4947
		private static readonly MainObject _Instance = new MainObject();

		// Token: 0x04001354 RID: 4948
		public static int IdMapEnd;

		// Token: 0x04001355 RID: 4949
		private static List<int> WayXmap;

		// Token: 0x04001356 RID: 4950
		private static int IndexWay;

		// Token: 0x04001357 RID: 4951
		private static bool IsNextMapFailed;

		// Token: 0x04001358 RID: 4952
		private static bool IsWait;

		// Token: 0x04001359 RID: 4953
		private static long TimeStartWait;

		// Token: 0x0400135A RID: 4954
		private static long TimeWait;

		// Token: 0x0400135B RID: 4955
		private static bool IsWaitNextMap;
	}
}
