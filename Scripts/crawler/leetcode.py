from selenium import webdriver
from bs4 import BeautifulSoup
browser = webdriver.PhantomJS()
url = 'https://leetcode.com/problems/house-robber/description/'
browser.get(url)
soup = BeautifulSoup(browser.page_source, 'lxml')
desc = soup.find("meta",  attrs={'name':'description'})
print(desc["content"] if desc else "No meta desc given")
browser.quit()