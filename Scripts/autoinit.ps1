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
$COMMENT += "    public class Solution$NUM`n"
$COMMENT += "    {`n"
$COMMENT += "`n"
$COMMENT += "    }`n"
$COMMENT += "}`n"
$COMMENT > ../Algorithms/$FILE

"|$NUM|[$TITLE]($URL) | [C#](./Algorithms/$FILE)|$DIFFCULT|" | Out-File -Encoding "utf8" -Append ../README.md