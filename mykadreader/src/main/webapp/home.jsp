<%@taglib prefix="s" uri="/struts-tags"%>
<!DOCTYPE html 
     PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"
     "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Tabuk Tech Mykad Reader Demo</title>

<link type="text/css"
	href="<s:url value="/static/js/jquery-ui/themes/base/jquery-ui.css" />"
	rel="stylesheet" />
</head>
<body>
<s:form action="saveOrUpdate" theme="simple">
	<table style="width: 600px">
		<thead>
			<tr>
				<th colspan="3">Tabuk Tech Mykad Reader Demo</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td colspan="3">
				<button style="width: 400px" id="buttonRead" type="button">Read</button>
				</td>
			</tr>
			<tr>
				<td>Name :</td>
				<td style="width: 350px"><s:textfield name="name" id="name"
					cssStyle="width: 300px" /></td>

				<td rowspan="7"><img
					src="<s:url value="/static/img/blank.jpg" />" alt="image"
					width="150" height="200" /></td>
			</tr>
			<tr>
				<td>IC Number:</td>
				<td><s:textfield name="id" id="id" /></td>

			</tr>
			<tr>
				<td>Religion:</td>
				<td><s:textfield name="religion" id="religion" /></td>
			</tr>
			<tr>
				<td>Race:</td>
				<td><s:textfield name="race" id="race" /></td>

			</tr>
			<tr>
				<td>Date of Birth:</td>
				<td><s:textfield name="dob" id="dob" /></td>

			</tr>
			<tr>
				<td>Gender:</td>
				<td><s:textfield name="gender" id="gender" /></td>

			</tr>
			<tr>
				<td>Nationality:</td>
				<td><s:textfield name="nationality" id="nationality" /></td>

			</tr>
			<tr>
				<td>Address :</td>
				<td><s:textarea cols="40" rows="4" name="address" id="address" /></td>

			</tr>
		</tbody>
	</table>
</s:form>
<div id="progress-bar"></div>
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
<script type="text/javascript"
	src="<s:url value="/static/js/jquery-ui/ui/jquery-ui.js" />"></script>
<script type="text/javascript">
	function read() {
		$("#progress-bar").progressbar( {
			value : 37
		});
	}
	function init_page()
	{
		
	}
	jQuery(document).ready(init_page);
</script>
</body>
</html>

