namespace Gabriel.Cat.S.Extension{

public static class PointerExtension{

public static unsafe byte[] ReadLine(this byte*[] ptrs,bool ptrNext=true){

byte[] bytesLine=new byte[ptrs.Length];
byte* ptrBytes;
fixed(byte* ptBytes=bytesLine){
ptrBytes=ptBytes;
for(int i=0;i<ptrs.Length;i++)
{
*ptrBytes=*ptrs[i];
ptrBytes++;
if(ptrNext)
ptrs[i]++;


}}
return bytesLine;




}
public static unsafe void WriteLine(this byte*[] ptrs,byte[] data,bool ptrNext=true){

fixed(byte* ptData=data)
     ptrs.WriteLine(ptData,ptrNext);

}
public static unsafe void WriteLine(this byte*[] ptrs,byte* ptrData,bool ptrNext=true){



for(int i=0;i<ptrs.Length;i++)
{
*ptrs[i]=*ptrData;
ptrData++;
if(ptrNext)
ptrs[i]++;


}

public static unsafe byte*[] ToArray(this byte* ptrData,int lengthPart,int parts){

byte*[] partes=new byte*[parts];
for(int i=0;i<parts;i++){
parts[i]=ptr;
ptr+=lengthPart;
}
return partes;
}



}



}




}
