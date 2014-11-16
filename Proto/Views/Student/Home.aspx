<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
    Student Writing Area (example)
<head>
	<meta charset="utf-8">
	<meta name="robots" content="noindex, nofollow">
	<title>Setting editor size</title>
	<script src="//cdn.ckeditor.com/4.4.5/full/ckeditor.js"></script>
</head>

<body style="
        background-image: url(http://encs.vancouver.wsu.edu/~k.gonzalez/letters3.50.jpg);
        background-repeat: no-repeat;
        background-size: cover">

	<textarea cols="80" id="editor1" name="editor1" rows="10" >
	</textarea>
	<script>
	    CKEDITOR.replace('editor1', {
	        width: '50%',
	        height: 100,
	        contentsCss: "body{background: url(http://encs.vancouver.wsu.edu/~t.roper/paper.jpg) no-repeat; background-size: cover;}"
	    });
	    
	</script>
</body>

</html>
