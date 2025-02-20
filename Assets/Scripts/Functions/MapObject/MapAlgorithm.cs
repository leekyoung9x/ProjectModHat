using System;
using System.Collections.Generic;

namespace MapObject
{
	// Token: 0x020000CE RID: 206
	public class MapAlgorithm
	{
		// Token: 0x06000A4F RID: 2639 RVA: 0x000A48D0 File Offset: 0x000A2AD0
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = MapAlgorithm.GetWayPassedStart(idMapStart);
			return MapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x000A48F0 File Offset: 0x000A2AF0
		private static List<int> FindWay(int idMapEnd, List<int> wayPassed)
		{
			int num = wayPassed[wayPassed.Count - 1];
			bool flag = num == idMapEnd;
			List<int> result;
			if (flag)
			{
				result = wayPassed;
			}
			else
			{
				bool flag2 = !ObjectData.Instance().CanGetMapNexts(num);
				if (flag2)
				{
					result = null;
				}
				else
				{
					List<List<int>> list = new List<List<int>>();
					foreach (NextMapObject mapNext in ObjectData.Instance().GetMapNexts(num))
					{
						List<int> list2 = null;
						bool flag3 = !wayPassed.Contains(mapNext.MapID);
						if (flag3)
						{
							List<int> wayPassedNext = MapAlgorithm.GetWayPassedNext(wayPassed, mapNext.MapID);
							list2 = MapAlgorithm.FindWay(idMapEnd, wayPassedNext);
						}
						bool flag4 = list2 != null;
						if (flag4)
						{
							list.Add(list2);
						}
					}
					result = MapAlgorithm.GetBestWay(list);
				}
			}
			return result;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000A49E0 File Offset: 0x000A2BE0
		private static List<int> GetBestWay(List<List<int>> ways)
		{
			bool flag = ways.Count == 0;
			List<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<int> list = ways[0];
				for (int i = 1; i < ways.Count; i++)
				{
					bool flag2 = MapAlgorithm.IsWayBetter(ways[i], list);
					if (flag2)
					{
						list = ways[i];
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000A4A44 File Offset: 0x000A2C44
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000A4A64 File Offset: 0x000A2C64
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000A4A84 File Offset: 0x000A2C84
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = MapAlgorithm.IsBadWay(way1);
			bool flag2 = MapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000A4AC8 File Offset: 0x000A2CC8
		private static bool IsBadWay(List<int> way)
		{
			return MapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000A4AE0 File Offset: 0x000A2CE0
		private static bool IsWayGoFutureAndBack(List<int> way)
		{
			List<int> list = new List<int>
			{
				27,
				28,
				29
			};
			for (int i = 1; i < way.Count - 1; i++)
			{
				bool flag = way[i] == 102 && way[i + 1] == 24 && list.Contains(way[i - 1]);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}
	}
}
