#include <iostream>
using namespace std;
int a[4]={0,1,2,3};
int num[10];
int main()
{
  // 请在此输入您的代码
  int n;
  cin >> n;
  while(n--){
    int b,c,x;
    cin >> b >> c >> x;
    swap(a[b],a[c]);
    num[a[x]]++;
  }
  cout << max(num[1],max(num[2],num[3]));
  return 0;
}

