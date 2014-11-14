<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
    Student Writing Area (example)
<head>
	<meta charset="utf-8">
	<meta name="robots" content="noindex, nofollow">
	<title>Setting editor size</title>
	<script src="http://cdn.ckeditor.com/4.4.5/standard-all/ckeditor.js"></script>
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
	        height: 100
	    });
	</script>
</body>

</html>
