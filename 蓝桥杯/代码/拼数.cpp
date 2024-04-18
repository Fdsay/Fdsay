#include <iostream>
#include<algorithm>
#include<string>
using namespace std;
string str[20];
bool st(string str1,string str2){
  return str1+str2>str2+str1;
}
int main()
{
  int n;
  cin>>n;
  for(int i=0;i<n;i++)
    cin>>str[i];
  sort(str,str+n,st);
  for(int i=0;i<n;i++)
    cout<<str[i];
  return 0;
}
