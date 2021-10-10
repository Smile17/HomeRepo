function setStyle() {
	var styleName = "style1.css"
 	var d = new Date();
	var n = d.getDay();
	if((n == 0) || (n==6))
		styleName = "style2.css";

	var file = location.pathname.split( "/" ).pop();

	var link = document.createElement( "link" );
	link.href = styleName;
	link.type = "text/css";
	link.rel = "stylesheet";
	link.media = "screen,print";
	console.log(link);
	document.getElementsByTagName( "head" )[0].appendChild( link );
}
setStyle();
