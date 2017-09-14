import requests
from bs4 import BeautifulSoup
import json
import time
import datetime
import sys
from config import *

def write_to_file(content):
    '''
    :param content:要写入文件的内容
    '''
    #print(content)
    #sys.exit()
    with open("result.txt",'a',encoding="utf-8") as f:
        f.write(json.dumps(content,ensure_ascii=False)+"\n")


def get_url(num):
    
    return "http://www.lottery.gov.cn/historykj/history_"+str(num)+".jspx?_ltype=dlt"
    
def parst_html(html):
    
    soup =BeautifulSoup(html,'lxml')
    div = soup.find('div',{'class':'result'})
    table=div.find("tbody")

    trs=table.find_all('tr')
    #trs = table.find("tr").find_next_sibling()
    #trs = table.find_next_siblings()
    #print(type(trs))
    #print(trs)    
    #sys.exit()
    for tr in trs:
        #print(type(tr))
        #print(tr)
        td=tr.find_all('td')
        
        
        #print(tds[0].text.strip())
        #sys.exit()
        yield[
            td[0].text.strip(),
            td[1].text.strip(),
            td[2].text.strip(),
            td[3].text.strip(),
            td[4].text.strip(),
            td[5].text.strip(),
            td[6].text.strip(),
            td[7].text.strip()
        ]
        
def main():
    num = 1
    while num<=ALL_NUM:
        base_url =get_url(num)
        print(base_url)
        num +=1
        #break
        request = requests.get(base_url)
        html=request.text

        res = parst_html(html)
        for i in res:
            write_to_file(i)
        time.sleep(2)
    else:
        print("ok")

               
if __name__ == '__main__':
    main()





