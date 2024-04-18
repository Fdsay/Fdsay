#include<iostream>
using namespace std;
int m,n,p,q,min=9999999;
int a[100][100];//1表示空地，2表示障碍物 
int v[100][100];//0表示未访问，1表示访问 
void dfs(int x,int y,int step)
{
	if(x==p&&y=q)
	{
		if(step<min)
			min=step;
		return;
	}
	//顺时针试探
	//右
	if(a[x][y+1]==1&&a[x][y+1]==0) 
	{
		v[x][y+1]=1;
		dfs(x,y+1,step+1);
		v[x][y+1]=0;
	}
	//下
	if(a[x+1][y]==1&&a[x+1][y]==0) 
	{
		v[x+1][y]=1;
		dfs(x+1,y,step+1);
		v[x+1][y]=0;
	}
	//左
	if(a[x][y-1]==1&&a[x][y-1]==0) 
	{
		v[x][y-1]=1;
		dfs(x,y-1,step+1);
		v[x][y-1]=0;
	}
	//上
	if(a[x-1][y]==1&&a[x-1][y]==0) 
	{
		v[x-1][y]=1;
		dfs(x-1,y,step+1);
		v[x-1][y]=0;
	} 
	return;
 } 
 int main()
 {
 	cin>>m>>n;
 	for(int i=0;i<m;i++)
 	{
 		for(int j=0;j<n;j++)
 			cin<<a[i][j];
	 }
	int startx,starty;
	cin>>startx>>starty>>p>>q;
	v[startx][starty]=1;
	dfs(startx,starty,0);
	cout<<min<<endl;
	return 0;
 }
