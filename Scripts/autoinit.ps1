$URL = $args[0]
$gitArgs = @("config", "--get", "user.name")
$command = &"git" $gitArgs
$COMMENT_TAG = '//'
$COMMENT = ""
$HTML = Invoke-WebRequest -Uri $URL
$QuestionContent = ($HTML.ParsedHtml.getElementsByTagName(‘div’) | Where{ $_.className -eq ‘question-content’ } ).innerText.Split("`n")
$QuestionTitle = (($HTML.ParsedHtml.getElementsByTagName(‘div’) | Where{ $_.className -eq ‘question-title clearfix’ })).innerText.Split("`n")[0]
$DIFFCULT = ($HTML.ParsedHtml.getElementsByTagName(‘div’) | Where{ $_.className -eq ‘question-info text-info’ } ).innerText.Split("`n")[2].Split(":")[1].Trim()
$AUTHOR = $command
$CURRENT_DATE = (Get-Date).ToShortDateString()
$NUM = $QuestionTitle.Split(".")[0].Trim().PadLeft(3).Replace(" ", "0") 
$TITLE = $QuestionTitle.Split(".")[1].Trim().Replace("(", "_").Replace(")", "")
$FILE = $TITLE.Replace(" ", "") + ".cs"
$TESTFILE = $TITLE.Replace(" ", "") + "Test.cs"
$NG =  ($HTML.ParsedHtml.body.getElementsByTagName(‘div’) | where {$_.getAttributeNode('ng-controller').Value -eq 'AceCtrl as aceCtrl'} )
$JSON = $NG.getAttributeNode('ng-init').Value
$START = $JSON.IndexOf("[{")
$END = $JSON.IndexOf("]")
$JSON = $JSON.Remove($END - 1).Trim() + "]"
$CODE = $JSON.Substring($START)
$CSHARP = $CODE | ConvertFrom-Json 
$CLASS = ($CSHARP | where { $_.value -eq 'csharp' }).defaultCode
$CLASS = $CLASS.Insert($CLASS.IndexOf("Solution") + 8, $NUM)
$CLASS = $CLASS.Insert($CLASS.LastIndexOf("public") + 7, "static ")
$COMMENT += "$COMMENT_TAG Source : $URL `n"
$COMMENT += "$COMMENT_TAG Author : $AUTHOR `n"
$COMMENT += "$COMMENT_TAG Date : $CURRENT_DATE `n"
$COMMENT += "`n"
$COMMENT += "/***************************************************************************************`n"
$COMMENT += "*`n"

$TAIL = $QuestionContent.Length - 4
foreach($a in $QuestionContent[0 .. $TAIL] ){
    $COMMENT += $a.Insert(0,”* “) + "`n"
}
$COMMENT += "*`n"
$COMMENT += "**********************************************************************************/`n"
$COMMENT += "`n"
$COMMENT += "using System;`n"
$COMMENT += "using System.Collections.Generic;`n"
$COMMENT += "namespace Algorithms`n"
$COMMENT += "{`n"
$COMMENT += $CLASS
$COMMENT += "}`n"
$COMMENT > ../Algorithms/$FILE

$TESTCLASSNAME = $TITLE.Replace(" ", "") + "Test"
$TESTCLASS = "using Algorithms;`nusing Xunit;`nnamespace AlgorithmsTest`n{`n"
$TESTCLASS += "    public class $TESTCLASSNAME`n"
$TESTCLASS += "    {`n"
$TESTCLASS += "        [Theory]`n"
$TESTCLASS += "        [InlineData()]`n"
$TESTCLASS += "        public void TestMethod(var output)`n"
$TESTCLASS += "        {`n" 
$TESTCLASS += "            Assert.Equal(output, Solution$NUM.var());`n" 
$TESTCLASS += "        }`n" 
$TESTCLASS += "    }`n"
$TESTCLASS += "}`n" 
$TESTCLASS > ../AlgorithmsTest/$TESTFILE

"|$NUM|[$TITLE]($URL) | [C#](./Algorithms/$FILE)|$DIFFCULT|" | Out-File -Encoding "utf8" -Append ../README.md