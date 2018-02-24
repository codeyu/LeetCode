from selenium import webdriver

browser = webdriver.PhantomJS()
url = 'https://leetcode.com/problems/house-robber/description/'
browser.get(url)
desc = browser.find_elements_by_xpath('//meta[@name="description"]') 
browser.find_element_by_xpath("//div[@class='Select-control']").click()
print browser.find_element_by_xpath("//div[@class='Select-control']").text
code_bak = browser.find_elements_by_xpath('//textarea[@name="lc-codemirror"]')
code = browser.find_elements_by_xpath('//pre[@class=" CodeMirror-line "]/span')
for t in desc:
    print t.get_attribute("content")
for c in code:
    print c.text
browser.quit()