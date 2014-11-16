﻿<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
	<meta name="viewports" content="noindex, nofollow">
	<title>Student Main Page</title>
	<script src="//cdn.ckeditor.com/4.4.5/full/ckeditor.js"></script>
</head>
<body style="
        background-image: url(http://encs.vancouver.wsu.edu/~k.gonzalez/letters3.50.jpg);
        background-repeat: no-repeat;
        background-size: cover">

        Educational Video Regarding the Writing Assignment
        <br>
        <asp:Label runat="server">
            <iframe width="500" height="300" src="//www.youtube.com/embed/pQcnLGBi7WE" frameborder="0" allowfullscreen></iframe>
        </asp:Label>
        <br><br>
    <form>
        <textarea cols="80" id="editor1" name="editor1" rows="10" >
	</textarea>
	<script>
	    CKEDITOR.replace('editor1', {
	        width: '80%',
	        height: 200,
	        contentsCss: "body{background: url(http://encs.vancouver.wsu.edu/~t.roper/paper.jpg) no-repeat; background-size: cover;}",
	        toolbar: [
                { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Save', 'NewPage'] },
                { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', '-', 'Undo', 'Redo'] },
                { name: 'editing', groups: ['find', 'selection'], items: ['Find', '-', 'SelectAll', '-'] },
                { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar', 'PageBreak'] },
                { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl'] },
                '/',
                { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
                { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
                { name: 'colors', items: ['TextColor', 'BGColor'] },
                { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
                { name: 'others', items: ['-'] },
	        ]
	    });
	   
	</script>
    </form>
    <div>
        
    </div>
</body>
</html>