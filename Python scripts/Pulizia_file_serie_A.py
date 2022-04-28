with(open('C:\\Users\\MicM\\Desktop\\Script python\\SerieA.txt','r') as Reader, open('SerieApulito.txt','a') as writer):
    da_pulire=Reader.readlines()
    for riga in da_pulire:
        if riga != '\n':
            if riga[0]!=' ' or "GIORNATA" in riga:
                if "GIORNATA" in riga and riga[0]!='G':
                    newriga=riga[1:]
                    newriga=newriga[10::-1][::-1]
                    newriga=newriga+'\n'
                    writer.write(newriga)
                else:
                    writer.write(riga)     