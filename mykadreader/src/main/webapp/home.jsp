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
<link type="text/css" href="<s:url value="/static/css/home.css" />"
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
				<button style="width: 400px" id="button-read" type="button">Read</button>
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
				<td><s:select name="gender" id="gender"
					list="#{'P':'Perempuan','L':'Lelaki'}" /></td>

			</tr>
			<tr>
				<td>Nationality:</td>
				<td><s:textfield name="nationality" id="nationality" /></td>

			</tr>
			<tr>
				<td>Address :</td>
				<td><s:textarea cols="40" rows="5" name="address" id="address" /></td>

			</tr>
		</tbody>
	</table>
</s:form>
<s:form id="pullImageAction" action="pullImageAction" theme="simple"></s:form>
<s:form id="pushImageAction" action="pushImageAction" theme="simple"
	method="post">
	<s:textfield name="encodedImageForm.blockCount" id="blockCount"
		value="16" />
	<s:textfield name="encodedImageForm.id" id="imageId" />
	<s:textfield name="encodedImageForm.blocks[0]" id="imageBlock0" />
	<s:textfield name="encodedImageForm.blocks[1]" id="imageBlock1" />
	<s:textfield name="encodedImageForm.blocks[2]" id="imageBlock2" />
	<s:textfield name="encodedImageForm.blocks[3]" id="imageBlock3" />
	<s:textfield name="encodedImageForm.blocks[4]" id="imageBlock4" />
	<s:textfield name="encodedImageForm.blocks[5]" id="imageBlock5" />
	<s:textfield name="encodedImageForm.blocks[6]" id="imageBlock6" />
	<s:textfield name="encodedImageForm.blocks[7]" id="imageBlock7" />
	<s:textfield name="encodedImageForm.blocks[8]" id="imageBlock8" />
	<s:textfield name="encodedImageForm.blocks[9]" id="imageBlock9" />
	<s:textfield name="encodedImageForm.blocks[10]" id="imageBlock10" />
	<s:textfield name="encodedImageForm.blocks[11]" id="imageBlock11" />
	<s:textfield name="encodedImageForm.blocks[12]" id="imageBlock12" />
	<s:textfield name="encodedImageForm.blocks[13]" id="imageBlock13" />
	<s:textfield name="encodedImageForm.blocks[14]" id="imageBlock14" />
	<s:textfield name="encodedImageForm.blocks[15]" id="imageBlock15" />
</s:form>
<div id="progress-dialog-parent">
<div id="progress-dialog" class="progress-dialog">
<div id="progress-bar"></div>
<div id="progress-status"></div>
</div>
</div>
<script type="text/javascript"
	src="<s:url value="/static/js/jquery-1.3.2.js" />"></script>
<script type="text/javascript"
	src="<s:url value="/static/js/jquery-ui/ui/jquery-ui.js" />"></script>
<script type="text/javascript"
	src="<s:url value="/static/js/jquery.form.js" />"></script>
<script type="text/javascript"
	src="<s:url value="/static/js/home.js" />"></script>
</body>
</html>

