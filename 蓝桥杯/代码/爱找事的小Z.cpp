#include<iostream>
#include<string>
#include<vector>
using namespace std;
int main() 
{
	int n,is=0,end=0;
	cin>>n;
	vector<string> str;
	for(int i=0;i<n;i++)
	{
		string s;
		cin>>s;
		str.push_back(s);
	}
	for(int i=0;i<n;i++)
	{
		for(int j=i+1;j<n;j++)
		{
			if(str[i]==str[j])
			{
				is=1;
				end=1;
				break;	
			}	
		}
		if(end==1)
			break;
	}
	cout<<is<<endl;
	return 0;
}
