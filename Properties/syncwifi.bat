echo %1
cd P:\ob\sportidenreader\cs\GecoSI.Net-master\src\pisireader\bin\Debug\
call rsync  -v -e "ssh  -i c:\uti\paf2private " *  root@192.168.193.1:/home/pi/simaster/
plink -i c:\uti\paf2private.ppk root@192.168.193.1 killall mono
exit 0