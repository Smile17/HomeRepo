function setStyle() {
	var styleName = "style1.css"
 	var width = screen.width;
	console.log(width);
	if(width == 640)
		styleName = "style1.css";
	if(width == 800)
		styleName = "style2.css";
	if(width == 1024)
		styleName = "style3.css";

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
