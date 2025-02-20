using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MapObject
{
    // Token: 0x020000D0 RID: 208
    public class ObjectData
    {
        // Token: 0x06000A72 RID: 2674 RVA: 0x0000672D File Offset: 0x0000492D
        private ObjectData()
        {
            this.GroupMaps = new List<ObjectGroups>();
            this.MyLinkMaps = null;
            this.IsLoading = false;
            this.IsLoadingCapsule = false;
        }

        // Token: 0x06000A73 RID: 2675 RVA: 0x000A5464 File Offset: 0x000A3664
        public static ObjectData Instance()
        {
            bool flag = ObjectData._Instance == null;
            if (flag)
            {
                ObjectData._Instance = new ObjectData();
            }
            return ObjectData._Instance;
        }

        // Token: 0x06000A74 RID: 2676 RVA: 0x00006757 File Offset: 0x00004957
        public void LoadLinkMaps()
        {
            this.IsLoading = true;
        }

        // Token: 0x06000A75 RID: 2677 RVA: 0x000A5494 File Offset: 0x000A3694
        public void Update()
        {
            bool isLoadingCapsule = this.IsLoadingCapsule;
            if (isLoadingCapsule)
            {
                bool flag = !this.IsWaitInfoMapTrans();
                if (flag)
                {
                    this.LoadLinkMapCapsule();
                    this.IsLoadingCapsule = false;
                    this.IsLoading = false;
                }
            }
            else
            {
                this.LoadLinkMapBase();
                bool flag2 = ObjectData.CanUseCapsuleVip();
                if (flag2)
                {
                    MainObject.UseCapsuleVip();
                    this.IsLoadingCapsule = true;
                }
                else
                {
                    bool flag3 = ObjectData.CanUseCapsuleNormal();
                    if (flag3)
                    {
                        MainObject.UseCapsuleNormal();
                        this.IsLoadingCapsule = true;
                    }
                    else
                    {
                        this.IsLoading = false;
                    }
                }
            }
        }

        public void LoadGroupMapsFromFile()
        {
            List<int> idMaps = new List<int>();
            int status = 0;
            try
            {
                idMaps = new List<int>
                {
                    44, 23, 14, 15 ,16, 17, 18, 20, 19 ,35 ,36 ,37, 38, 26, 52, 84, 129
                };
                status = 11;
                MapData.XmapData map2 = new MapData.XmapData(idMaps, "Xayda");
                status = 12;
                idMaps = new List<int>
            {
                42,21,0,1,2,3,4,5,6,27,28,29,30,47,46,45,48,50,111,24,53,58,59,60,61,62,55,56,54,57
            };
                MapData.XmapData map3 = new MapData.XmapData(idMaps, "Trái Đất");
                status = 2;
                idMaps = new List<int>
            {
                43, 22, 7 ,8 ,9 ,11, 12, 13, 10, 31, 32 ,33 ,34 ,25
            };
                MapData.XmapData map4 = new MapData.XmapData(idMaps, "Namek");
                status = 3;
                idMaps = new List<int>
            {
                68,69,70,71,72,64,65,63,66,67,73,74,75,76,77,81,82,83,79,80,131,132,133
            };
                MapData.XmapData map5 = new MapData.XmapData(idMaps, "Nappa");
                status = 4;
                idMaps = new List<int>
            {
                102,92,93,94,96,97,98,99,100,103
            };
                MapData.XmapData map6 = new MapData.XmapData(idMaps, "Tương Lai");
                status = 5;
                idMaps = new List<int>
            {
                109,108,107,110,106,105
            };
                this.GroupMaps.Clear();
                status = 9;
                try
                {
                    GroupMaps.Add(new ObjectGroups(map2.mapname, map2.datamap));
                    GroupMaps.Add(new ObjectGroups(map3.mapname, map3.datamap));
                    GroupMaps.Add(new ObjectGroups(map4.mapname, map4.datamap));
                    GroupMaps.Add(new ObjectGroups(map5.mapname, map5.datamap));
                    GroupMaps.Add(new ObjectGroups(map6.mapname, map6.datamap));
                    status = 10;
                }
                catch (Exception ex)
                {
                    GameScr.info1.addInfo(ex.Message, 0);
                }
                this.RemoveMapsHomeInGroupMaps();
            }
            catch (Exception ex)
            {
               
            }
        }

        // Token: 0x06000A77 RID: 2679 RVA: 0x000A5608 File Offset: 0x000A3808
        private void RemoveMapsHomeInGroupMaps()
        {
            int cgender = global::Char.myCharz().cgender;
            foreach (ObjectGroups groupMap in this.GroupMaps)
            {
                bool flag = cgender != 0;
                if (flag)
                {
                    bool flag2 = cgender != 1;
                    if (flag2)
                    {
                        groupMap.IdMaps.Remove(21);
                        groupMap.IdMaps.Remove(22);
                    }
                    else
                    {
                        groupMap.IdMaps.Remove(21);
                        groupMap.IdMaps.Remove(23);
                    }
                }
                else
                {
                    groupMap.IdMaps.Remove(22);
                    groupMap.IdMaps.Remove(23);
                }
            }
        }

        // Token: 0x06000A78 RID: 2680 RVA: 0x000A56DC File Offset: 0x000A38DC
        private void LoadLinkMapCapsule()
        {
            this.AddKeyLinkMaps(TileMap.mapID);
            string[] mapNames = GameCanvas.panel.mapNames;
            for (int i = 0; i < mapNames.Length; i++)
            {
                int idMapFromName = ObjectData.GetIdMapFromName(mapNames[i]);
                bool flag = idMapFromName != -1;
                if (flag)
                {
                    int[] info = new int[]
                    {
                        i
                    };
                    this.MyLinkMaps[TileMap.mapID].Add(new NextMapObject(idMapFromName, MapEnum.Capsule, info));
                }
            }
        }

        // Token: 0x06000A79 RID: 2681 RVA: 0x00006761 File Offset: 0x00004961
        private void LoadLinkMapBase()
        {
            this.MyLinkMaps = new Dictionary<int, List<NextMapObject>>();
            this.LoadLinkMapsFromFile();
            this.LoadLinkMapsAutoWaypointFromFile();
            this.LoadLinkMapsHome();
            this.LoadLinkMapSieuThi();
            this.LoadLinkMapToCold();
        }

        public string[] DataXMap = new string[]
        {
            "24 25 1 10 0",
"25 24 1 11 0",
"45 48 1 19 3",
"48 45 1 20 3 0",
"48 50 1 20 3 1",
"50 48 1 44 0",
"24 26 1 10 1",
"26 24 1 12 0",
"25 26 1 11 1",
"26 25 1 12 1",
"24 84 1 10 2",
"25 84 1 11 2",
"26 84 1 12 2",
"19 68 1 12 1",
"68 19 1 12 0",
"80 131 1 60 0",
"27 102 1 38 1",
"28 102 1 38 1",
"29 102 1 38 1",
"102 24 1 38 1",
"27 53 1 25 0",
"52 129 1 23 3",
"0 149 1 67 3 0",
"45 48 1 19 3",
"50 48 1 44 0",
"139 25 1 63 1",
"139 26 1 63 2",
"24 139 1 63 0",
"139 24 1 63 0",
"19 126 1 53 0",
"126 19 1 53 0",
"24 139 1 63 0",
"139 24 1 63 0",
"19 126 1 53 0",
"126 19 1 53 0",
"0 122 1 49 0",
"80 160 1 60 0"
        };

        private void LoadLinkMapsFromFile()
        {

            try
            {
                for (int i = 0; i < this.DataXMap.Length; i++)
                {
                    string text;
                    bool flag = (text = this.DataXMap[i]) == null;
                    if (!flag)
                    {
                        text = text.Trim();
                        bool flag2 = !text.StartsWith("#") && !text.Equals("");
                        if (flag2)
                        {
                            int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
                            {
                                ' '
                            }), (string s) => int.Parse(s));
                            int num = array.Length - 3;
                            int[] array2 = new int[num];
                            Array.Copy(array, 3, array2, 0, num);
                            this.LoadLinkMap(array[0], array[1], (MapEnum)array[2], array2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }

        // Token: 0x06000A7B RID: 2683 RVA: 0x000A5888 File Offset: 0x000A3A88
        public string[] DataWayPoint = new string[]
{
            "42 0 1 2 3 4 5 6",
            "3 27 28 29 30",
            "2 24",
            "1 47 46 45",
            "5 29",
            "47 111",
            "53 58 59 60 61 62 55 56 54 57",
            "53 27",
            "43 7 8 9 11 12 13 10",
            "11 31 32 33 34",
            "9 25",
            "13 33",
            "52 44 14 15 16 17 18 20 19",
            "17 35 36 37 38",
            "16 26",
            "20 37",
            "68 69 70 71 72 64 65 63 66 67 73 74 75 76 77 81 82 83 79 80",
            "102 92 93 94 96 97 98 99 100 103",
            "109 108 107 110 106",
            "109 105",
            "109 106",
            "106 107",
            "108 105",
            "131 132 133",
            "80 105",
            "160 161 162 163",
            "139 140",
            "149 147 152 151 148",
            "122 123 124"
};
        private void LoadLinkMapsAutoWaypointFromFile()
        {
            try
            {
                for (int z = 0; z < this.DataWayPoint.Length; z++)
                {
                    string text;
                    if ((text = this.DataWayPoint[z]) != null)
                    {
                        text = text.Trim();
                        if (!text.StartsWith("#") && !text.Equals(""))
                        {
                            int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
                            {
                                ' '
                            }), (string s) => int.Parse(s));
                            for (int i = 0; i < array.Length; i++)
                            {
                                if (i != 0)
                                {
                                    this.LoadLinkMap(array[i], array[i - 1], MapEnum.AutoWaypoint, null);
                                }
                                if (i != array.Length - 1)
                                {
                                    this.LoadLinkMap(array[i], array[i + 1], MapEnum.AutoWaypoint, null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GameScr.info1.addInfo(ex.Message, 0);
            }
        }

        // Token: 0x06000A7C RID: 2684 RVA: 0x000A59AC File Offset: 0x000A3BAC
        private void LoadLinkMapsHome()
        {
            int cgender = global::Char.myCharz().cgender;
            int num = 21 + cgender;
            int num2 = 7 * cgender;
            this.LoadLinkMap(num2, num, MapEnum.AutoWaypoint, null);
            this.LoadLinkMap(num, num2, MapEnum.AutoWaypoint, null);
        }

        // Token: 0x06000A7D RID: 2685 RVA: 0x000A59E4 File Offset: 0x000A3BE4
        private void LoadLinkMapSieuThi()
        {
            int cgender = global::Char.myCharz().cgender;
            int idMapNext = 24 + cgender;
            int[] array = new int[2];
            array[0] = 10;
            int[] info = array;
            this.LoadLinkMap(84, idMapNext, MapEnum.NpcMenu, info);
        }

        // Token: 0x06000A7E RID: 2686 RVA: 0x000A5A1C File Offset: 0x000A3C1C
        private void LoadLinkMapToCold()
        {
            bool flag = global::Char.myCharz().taskMaint.taskId > 30;
            if (flag)
            {
                int[] array = new int[2];
                array[0] = 12;
                int[] info = array;
                this.LoadLinkMap(19, 109, MapEnum.NpcMenu, info);
            }
        }

        // Token: 0x06000A7F RID: 2687 RVA: 0x000A5A60 File Offset: 0x000A3C60
        public List<NextMapObject> GetMapNexts(int idMap)
        {
            bool flag = this.CanGetMapNexts(idMap);
            List<NextMapObject> result;
            if (flag)
            {
                result = this.MyLinkMaps[idMap];
            }
            else
            {
                result = null;
            }
            return result;
        }

        // Token: 0x06000A80 RID: 2688 RVA: 0x000A5A90 File Offset: 0x000A3C90
        public bool CanGetMapNexts(int idMap)
        {
            return this.MyLinkMaps.ContainsKey(idMap);
        }

        // Token: 0x06000A81 RID: 2689 RVA: 0x000A5AB0 File Offset: 0x000A3CB0
        private void LoadLinkMap(int idMapStart, int idMapNext, MapEnum type, int[] info)
        {
            this.AddKeyLinkMaps(idMapStart);
            NextMapObject item = new NextMapObject(idMapNext, type, info);
            this.MyLinkMaps[idMapStart].Add(item);
        }

        // Token: 0x06000A82 RID: 2690 RVA: 0x000A5AE4 File Offset: 0x000A3CE4
        private void AddKeyLinkMaps(int idMap)
        {
            bool flag = !this.MyLinkMaps.ContainsKey(idMap);
            if (flag)
            {
                this.MyLinkMaps.Add(idMap, new List<NextMapObject>());
            }
        }

        // Token: 0x06000A83 RID: 2691 RVA: 0x000A5B1C File Offset: 0x000A3D1C
        private bool IsWaitInfoMapTrans()
        {
            return !MapService.IsShowPanelMapTrans;
        }

        // Token: 0x06000A84 RID: 2692 RVA: 0x000A5B38 File Offset: 0x000A3D38
        public static int GetIdMapFromPanelXmap(string mapName)
        {
            return int.Parse(mapName.Split(new char[]
            {
                ':'
            })[0]);
        }

        // Token: 0x06000A85 RID: 2693 RVA: 0x000A5B64 File Offset: 0x000A3D64
        public static Waypoint FindWaypoint(int idMap)
        {
            int i = 0;
            while (i < TileMap.vGo.size())
            {
                Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                bool flag = MainObject.IdMapEnd == 124 && TileMap.mapID == 123;
                if (flag)
                {
                    for (int j = 0; j < TileMap.vGo.size(); j++)
                    {
                        Waypoint result = (Waypoint)TileMap.vGo.elementAt(j);
                        bool flag2 = j == TileMap.vGo.size() - 1;
                        if (flag2)
                        {
                            return result;
                        }
                    }
                }
                bool flag3 = ObjectData.GetTextPopup(waypoint.popup).Trim().ToLower().Equals(MainObject.get_map_names(idMap).Trim().ToLower());
                if (!flag3)
                {
                    i++;
                    continue;
                }
                return waypoint;
            }
            return null;
        }

        // Token: 0x06000A86 RID: 2694 RVA: 0x000A5C48 File Offset: 0x000A3E48
        public static int GetPosWaypointX(Waypoint waypoint)
        {
            bool flag = waypoint.maxX < 60;
            int result;
            if (flag)
            {
                result = 15;
            }
            else
            {
                bool flag2 = (int)waypoint.minX > TileMap.pxw - 60;
                if (flag2)
                {
                    result = TileMap.pxw - 15;
                }
                else
                {
                    result = (int)(waypoint.minX + 30);
                }
            }
            return result;
        }

        // Token: 0x06000A87 RID: 2695 RVA: 0x000A5C98 File Offset: 0x000A3E98
        public static int GetPosWaypointY(Waypoint waypoint)
        {
            return (int)waypoint.maxY;
        }

        // Token: 0x06000A88 RID: 2696 RVA: 0x000A5CB0 File Offset: 0x000A3EB0
        public static bool IsMyCharDie()
        {
            return global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0;
        }

        // Token: 0x06000A89 RID: 2697 RVA: 0x000A5CE4 File Offset: 0x000A3EE4
        public static bool CanNextMap()
        {
            return !global::Char.isLoadingMap && !global::Char.ischangingMap && !Controller.isStopReadMessage;
        }

        // Token: 0x06000A8A RID: 2698 RVA: 0x000A5D10 File Offset: 0x000A3F10
        private static int GetIdMapFromName(string mapName)
        {
            int cgender = global::Char.myCharz().cgender;
            bool flag = mapName.Equals("Về nhà");
            int result;
            if (flag)
            {
                result = 21 + cgender;
            }
            else
            {
                bool flag2 = mapName.Equals("Trạm tàu vũ trụ");
                if (flag2)
                {
                    result = 24 + cgender;
                }
                else
                {
                    bool flag3 = mapName.Contains("Về chỗ cũ: ");
                    if (flag3)
                    {
                        mapName = mapName.Replace("Về chỗ cũ: ", "");
                        bool flag4 = MainObject.get_map_names(MapService.IdMapCapsuleReturn).Equals(mapName);
                        if (flag4)
                        {
                            return MapService.IdMapCapsuleReturn;
                        }
                        bool flag5 = mapName.Equals("Rừng đá");
                        if (flag5)
                        {
                            return -1;
                        }
                    }
                    for (int i = 0; i < TileMap.mapNames.Length; i++)
                    {
                        bool flag6 = mapName.Equals(MainObject.get_map_names(i));
                        if (flag6)
                        {
                            return i;
                        }
                    }
                    result = -1;
                }
            }
            return result;
        }

        // Token: 0x06000A8B RID: 2699 RVA: 0x000A5DF4 File Offset: 0x000A3FF4
        public static string GetTextPopup(PopUp popUp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < popUp.says.Length; i++)
            {
                stringBuilder.Append(popUp.says[i]);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }

        // Token: 0x06000A8C RID: 2700 RVA: 0x000A5E4C File Offset: 0x000A404C
        private static bool CanUseCapsuleNormal()
        {
            return !ObjectData.IsMyCharDie() && MapService.IsUseCapsuleNormal && ObjectData.HasItemCapsuleNormal();
        }

        // Token: 0x06000A8D RID: 2701 RVA: 0x000A5E74 File Offset: 0x000A4074
        private static bool HasItemCapsuleNormal()
        {
            Item[] arrItemBag = global::Char.myCharz().arrItemBag;
            for (int i = 0; i < arrItemBag.Length; i++)
            {
                bool flag = arrItemBag[i] != null && arrItemBag[i].template.id == 193;
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x06000A8E RID: 2702 RVA: 0x000A5ED0 File Offset: 0x000A40D0
        private static bool CanUseCapsuleVip()
        {
            return !ObjectData.IsMyCharDie() && MapService.IsUseCapsuleVip && ObjectData.HasItemCapsuleVip();
        }

        // Token: 0x06000A8F RID: 2703 RVA: 0x000A5EF8 File Offset: 0x000A40F8
        private static bool HasItemCapsuleVip()
        {
            Item[] arrItemBag = global::Char.myCharz().arrItemBag;
            for (int i = 0; i < arrItemBag.Length; i++)
            {
                bool flag = arrItemBag[i] != null && arrItemBag[i].template.id == 194;
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0400135C RID: 4956
        private const int ID_MAP_HOME_BASE = 21;

        // Token: 0x0400135D RID: 4957
        private const int ID_MAP_TTVT_BASE = 24;

        // Token: 0x0400135E RID: 4958
        private const int ID_ITEM_CAPSUAL_VIP = 194;

        // Token: 0x0400135F RID: 4959
        private const int ID_ITEM_CAPSUAL_NORMAL = 193;

        // Token: 0x04001360 RID: 4960
        private const int ID_MAP_TPVGT = 19;

        // Token: 0x04001361 RID: 4961
        private const int ID_MAP_TO_COLD = 109;

        // Token: 0x04001362 RID: 4962
        public List<ObjectGroups> GroupMaps;

        // Token: 0x04001363 RID: 4963
        public Dictionary<int, List<NextMapObject>> MyLinkMaps;

        // Token: 0x04001364 RID: 4964
        public bool IsLoading;

        // Token: 0x04001365 RID: 4965
        private bool IsLoadingCapsule;

        // Token: 0x04001366 RID: 4966
        private static ObjectData _Instance;
    }
}
