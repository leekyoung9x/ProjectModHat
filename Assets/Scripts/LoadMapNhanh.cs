using System.Text;

public class LoadMapNhanh
{
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

	public static void RequestChangeMap(Waypoint waypoint)
	{
		if (waypoint.isOffline)
		{
			Service.gI().getMapOffline();
		}
		else
		{
			Service.gI().requestChangeMap();
		}
	}

	public static void MoveMyChar(int x, int y)
	{
		Char.myCharz().cx = x;
		Char.myCharz().cy = y;
		Service.gI().charMove();
		if (!ItemTime.isExistItem(4387))
		{
			Char.myCharz().cx = x;
			Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			Char.myCharz().cx = x;
			Char.myCharz().cy = y;
			Service.gI().charMove();
		}
	}

	public static Waypoint FindWaypoint(int type)
	{
		if (TileMap.vGo.size() == 1)
		{
			return (Waypoint)TileMap.vGo.elementAt(0);
		}
		for (int i = 0; i < TileMap.vGo.size(); i++)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
			if (type == 0)
			{
				if ((TileMap.mapID == 70 && GetTextPopup(waypoint.popup) == "Vực cấm") || (TileMap.mapID == 73 && GetTextPopup(waypoint.popup) == "Vực chết") || (TileMap.mapID == 110 && GetTextPopup(waypoint.popup) == "Rừng tuyết"))
				{
					return waypoint;
				}
				if (waypoint.maxX < 60)
				{
					return waypoint;
				}
			}
			if (type == 1)
			{
				if (((TileMap.mapID == 106 || TileMap.mapID == 107) && GetTextPopup(waypoint.popup) == "Hang băng") || ((TileMap.mapID == 105 || TileMap.mapID == 108) && GetTextPopup(waypoint.popup) == "Rừng băng") || (TileMap.mapID == 109 && GetTextPopup(waypoint.popup) == "Cánh đồng tuyết"))
				{
					return waypoint;
				}
				if (TileMap.mapID == 27)
				{
					return null;
				}
				if (waypoint.minX < TileMap.pxw - 60 && waypoint.maxX >= 60)
				{
					return waypoint;
				}
			}
			if (type == 2)
			{
				if (TileMap.mapID == 70 && GetTextPopup(waypoint.popup) == "Căn cứ Raspberry")
				{
					return waypoint;
				}
				if (waypoint.minX > TileMap.pxw - 60)
				{
					return waypoint;
				}
			}
		}
		return null;
	}

	public static void LoadMap(int type)
	{
		Waypoint waypoint = FindWaypoint(type);
		if (waypoint != null)
		{
			int maxY = waypoint.maxY;
			MoveMyChar((waypoint.maxX < 60) ? 15 : ((waypoint.minX <= TileMap.pxw - 60) ? (waypoint.minX + 30) : (TileMap.pxw - 15)), maxY);
			if (type == 1 || TileMap.vGo.size() == 1)
			{
				RequestChangeMap(waypoint);
			}
		}
	}
}
