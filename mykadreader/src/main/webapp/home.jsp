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
				<td>IC Number:</td>
				<s:text name="id" id="id" />
			</tr>
			<tr>
				<td>IC Number:</td>
				<s:text name="id" id="id" />
			</tr>
		</tbody>
	</table>
</s:form>
<s:form action="displayImage">
	<s:hidden name="blockCount" id="blockCount" value="16" />
	<s:hidden name="imageBlock0" id="imageBlock0" />
	<s:hidden name="imageBlock1" id="imageBlock1" />
	<s:hidden name="imageBlock2" id="imageBlock2" />
	<s:hidden name="imageBlock3" id="imageBlock3" />
	<s:hidden name="imageBlock4" id="imageBlock4" />
	<s:hidden name="imageBlock5" id="imageBlock5" />
	<s:hidden name="imageBlock6" id="imageBlock6" />
	<s:hidden name="imageBlock7" id="imageBlock7" />
	<s:hidden name="imageBlock8" id="imageBlock8" />
	<s:hidden name="imageBlock9" id="imageBlock9" />
	<s:hidden name="imageBlock0" id="imageBlock0" />
	<s:hidden name="imageBlock11" id="imageBlock11" />
	<s:hidden name="imageBlock12" id="imageBlock12" />
	<s:hidden name="imageBlock13" id="imageBlock13" />
	<s:hidden name="imageBlock14" id="imageBlock14" />
	<s:hidden name="imageBlock15" id="imageBlock15" />
</s:form>

<script type="text/javascript"
	src="<s:url value="/static/js/jquery-1.3.2.js" />"></script>
<script type="text/javascript">
	function read() {
	}
	jQuery(document).ready(read);
</script>
</body>
</html>

