with(open('C:\\Users\\MicM\\Desktop\\Script python\\SerieApulitopt3.txt','r') as Reader, open('SerieApulitopt4.txt','a') as writer):
    da_pulire=Reader.readlines()
    for line in da_pulire:
        if line[-3][0]==" ":
            writer.write(line[0:-3]+"-"+line[-2:])
        else:
            writer.write(line)
