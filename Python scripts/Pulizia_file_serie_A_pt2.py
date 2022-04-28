with(open('C:\\Users\\MicM\\Desktop\\Script python\\SerieApulito.txt','r') as Reader, open('SerieApulitopt2.txt','a') as writer):
    da_pulire=Reader.readlines()
    pulizia=False
    flag=False
    for riga in da_pulire:
        sol=riga
        val=riga[-2:][0]
        if pulizia and flag and "GIORNATA" not in riga:
            sol=sol.replace('\n','-')
            flag=False
        if riga=="GIORNATA 31\n":
            pulizia=True
            flag=True
        if riga[-2:][0] in "0123456789":
            flag=True
        writer.write(sol)