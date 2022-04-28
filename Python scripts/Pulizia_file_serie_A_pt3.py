with(open('C:\\Users\\MicM\\Desktop\\Script python\\SerieApulitopt2.txt','r') as Reader, open('SerieApulitopt3.txt','a') as writer):
    da_pulire=Reader.readlines()
    for line in da_pulire:
        if "GIORNATA" not in line and "(" not in line and line[-4:][0] in  "0123456789" and line[-5][0] != " " :
            writer.write(line[0:-4:1]+" "+line[-4::1])
        else:
            writer.write(line)