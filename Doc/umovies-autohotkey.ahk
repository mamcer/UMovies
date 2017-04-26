#IfWinExist Media Player Classic Home Cinema
^p:: 
WinActivate,
Send, {Space}
Return
^l:: 
WinActivate,
Send, {Left}
Return
^r:: 
WinActivate,
Send, {Right}
Return
^b:: 
WinActivate,
Send, {.}
Send, {Space}
#IfWinActive