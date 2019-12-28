namespace Gabriel.Cat.S.Extension{

public static class PointerExtension{

public static unsafe byte[] ReadLine(this byte*[] ptrs,bool ptrNext=true){

byte[] bytesLine=new byte[ptrs.Length];

for(int i=0;i<ptrs.Length;i++)
{
bytes[i]=*ptrs[i];
if(ptrNext)
ptrs[i]++;


}
return bytes;




}
public static unsafe void WriteLine(this byte*[] ptrs,byte[] data,bool ptrNext=true){

fixed(byte* ptData=data)
     ptrs.WriteLine(ptData,ptrNext);

}
public static unsafe void WriteLine(this byte*[] ptrs,byte* ptrData,bool ptrNext=true){



for(int i=0;i<ptrs.Length;i++)
{
*ptrs[i]=*ptrData;
data++;
if(ptrNext)
ptrs[i]++;


}





}



}




}
