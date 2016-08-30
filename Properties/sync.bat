echo %1
cd P:\ob\sportidenreader\cs\GecoSI.Net-master\src\pisireader\bin\Debug\
call rsync  -v -z -a -e "ssh  -i c:\uti\paf2private " *  root@192.168.25.44:/home/pi/simaster/
exit 0