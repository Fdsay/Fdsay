#include<iostream>
#include<vector>
#include<algorithm>
using namespace std;
int main()
{
	int m, n;
	cin >> m >> n;
	vector<vector<char> > array(m, vector<char>(n));
	for (int i = 0; i < m; i++)
	{
		for (int j = 0; j < n; j++)
			cin >> array[i][j];
	}
	int range = min(m, n);
	int size = 3;
	int times = 0;
	while (size <= range)
	{
		int xtimes = m - size;
		int ytimes = n - size;
		if(ytimes!=0&&xtimes!=0)
		{
			for (int i = 0; i < xtimes; i++)
			{
				for (int j = 0; j < ytimes; j++)
				{
					int r = i + size / 2;
					int c = j + size / 2;
					int L = size / 2;
					int is = 1;
					for (int k = 1; k < L + 1; k++)
					{
						if (array[r - k][c - k] != array[r][c] || array[r - k][c + k] != array[r][c] || array[r + k][c - k] != array[r][c] || array[r + k][c + k] != array[r][c])
						{
							is = 0;
							break;
						}
					}
					if (is == 1)
						times++;
				}
			}
		}
		else if (xtimes == 0)
		{
			for (int j = 0; j < ytimes; j++)
			{
				int r =  size / 2;
				int c = j + size / 2;
				int L = size / 2;
				int is = 1;
				for (int k = 1; k < L + 1; k++)
				{
					if (array[r - k][c - k] != array[r][c] || array[r - k][c + k] != array[r][c] || array[r + k][c - k] != array[r][c] || array[r + k][c + k] != array[r][c])
					{
						is = 0;
						break;
					}
				}
				if (is == 1)
					times++;
			}
		}
		else if(ytimes==0)
		{
			for (int i = 0; i < xtimes; i++)
			{
				int r = i + size / 2;
				int c = size / 2;
				int L = size / 2;
				int is = 1;
				for (int k = 1; k < L + 1; k++)
				{
					if (array[r - k][c - k] != array[r][c] || array[r - k][c + k] != array[r][c] || array[r + k][c - k] != array[r][c] || array[r + k][c + k] != array[r][c])
					{
						is = 0;
						break;
					}
				}
				if (is == 1)
					times++;
			}
		}
		else
		{
			int r =  size / 2;
			int c = size / 2;
			int L = size / 2;
			int is = 1;
			for (int k = 1; k < L + 1; k++)
			{
				if (array[r - k][c - k] != array[r][c] || array[r - k][c + k] != array[r][c] || array[r + k][c - k] != array[r][c] || array[r + k][c + k] != array[r][c])
				{
					is = 0;
					break;
				}
			}
			if (is == 1)
				times++;
		}
		size = size + 2;
	}
	cout << times << endl;
	return 0;
}
