// JScript source code

//contains calls to silverlight.js, example below loads Page.xaml
function createSilverlight()
{

	Silverlight.createObjectEx({
		source: "Page.xaml",
		parentElement: document.getElementById("SilverlightControlHost"),
		id: "SilverlightControl",
		properties: {
			width: "100%",
			height: "100%",
			version: "1.1",
			enableHtmlAccess: "true"
		},
		events: {}
	});
	   
	// Give the keyboard focus to the Silverlight control by default
    document.body.onload = function() {
      var silverlightControl = document.getElementById('SilverlightControl');
      if (silverlightControl)
      silverlightControl.focus();
    }

}

function onNewGameClick(players) {
   var silverlightControl = document.getElementById('SilverlightControl');
   if(silverlightControl)
   {
       silverlightControl.focus();
       silverlightControl.Content.SilverNibbles.NewGame(players);
   }
}

function onInitialSpeedChanged() {
   var silverlightControl = document.getElementById('SilverlightControl');
   var speedCombo = document.getElementById('InitialSpeedCombo');
   var index = speedCombo.selectedIndex;
   silverlightControl.Content.SilverNibbles.StartingSpeed = parseInt(speedCombo.options[index].value,10);
   silverlightControl.focus();
}


