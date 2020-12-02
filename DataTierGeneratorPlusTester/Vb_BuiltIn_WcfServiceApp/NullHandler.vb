'Note: as this module is based on the DotNetNuke file Null.vb, I have included this copyright notice.--SJS
'
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2006
' by Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'
Imports System
Imports System.Reflection

'NameSpace Vb_BuiltIn_WcfServiceApp

    Public NotInheritable Class NullHandler

        Shared Sub New()  
        End Sub

        ' convert db null  to app null 
        Public Shared Function HandleDbNull(ByVal objValue As Object) As Object 
            Dim returnValue As Object

            If (objValue Is System.DBNull.Value) Then

                'complex object
                returnValue = Nothing
            Else
                ' return value
                returnValue = objValue
            End If
            Return returnValue

        End Function

        ' convert app null  to db null 
        Public Shared Function HandleAppNull(ByVal objField As Object, ByVal objDBNull As Object) As Object
            Dim returnValue As Object = objField

            If (objField Is Nothing) Then
                returnValue = objDBNull
            Else
                returnValue = objField
            End If
            Return returnValue

        End Function

    End Class

'End NameSpace