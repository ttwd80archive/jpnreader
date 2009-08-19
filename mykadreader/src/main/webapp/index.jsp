<%@taglib prefix="s" uri="/struts-tags"%>
<!DOCTYPE html 
     PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
     "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Tabuk Tech Mykad Reader Demo</title>
</head>
<body>
<s:form action="saveOrUpdate" theme="simple">
	<table>
		<thead>
			<tr>
				<th colspan="3">Tabuk Tech Mykad Reader Demo</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td>Name :</td>
				<td><s:text name="name" id="name" /></td>

				<td rowspan="5"><img
					src="<s:url value="/static/img/no_picture.jpg" />" alt="image"
					width="200" height="150" /></td>
			</tr>
			<tr>
				<td>IC Number: </td>
				<s:text name="id" id="id" />
			</tr>
			<tr>
				<td>IC Number: </td>
				<s:text name="id" id="id" />
			</tr>
		</tbody>
	</table>
</s:form>
<form action="http://localhost:8080/mykadimage/image.do">
<div><input type="hidden" name="blockCount" id="blockCount"
	value="16" /> <input type="hidden" name="image1" id="image1" /> <input
	type="hidden" name="image2" id="image2" /> <input type="hidden"
	name="image3" id="image3" /> <input type="hidden" name="image4"
	id="image4" /> <input type="hidden" name="image5" id="image5" /> <input
	type="hidden" name="image6" id="image6" /> <input type="hidden"
	name="image7" id="image7" /> <input type="hidden" name="image8"
	id="image8" /> <input type="hidden" name="image9" id="image9" /> <input
	type="hidden" name="image10" id="image10" /> <input type="hidden"
	name="image11" id="image11" /> <input type="hidden" name="image12"
	id="image12" /> <input type="hidden" name="image13" id="image13" /> <input
	type="hidden" name="image14" id="image14" /> <input type="hidden"
	name="image15" id="image15" /></div>
</form>
<script type="text/javascript" src="jquery-1.3.2.js"></script>
<script type="text/javascript">
	function read() {
	}
	jQuery(document).ready(read);
</script>
</body>
</html>

