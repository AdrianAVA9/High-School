//validateSession();

$(document).ready(function () {

	// IF YOU WANT TO USE OWNER PASSWORD FLOW FROM JAVASCRIPT, YOU HAVE TO MAKE SOME CHANGES IN VIEWS AND CONTROLS
	
	$("#btnLogin").click(function () {

		if (!validateEmptyInput()) {
			if (validateToAvoidInvalidFormat()) {
				GetToken($("#txtUsername").val(), $("#txtPassword").val());
			} else {
				$("#errorMessage").text("The username or password does not have a correct format");
			}
		} else {
			$("#errorMessage").text("Please, complete the information");
		}
	});
});

function validateSession() {
	console.log(window.sessionStorage.length);
	console.log(window.sessionStorage);
	if (window.sessionStorage.length) {
		cosole.log("window.sessionStorage.length = " + window.sessionStorage.length);
	}
}

function validateToAvoidInvalidFormat() {
 
	//check that your format is like an email
	var username = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/;
	// Check password contains at least one digit, one lower case 
	// letter, one uppercase letter, and is between 8 and 10 
	// characters long
	var password = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,11}$/;
	var Error = false;

	if (username.test($("#txtUsername").val()) & password.test($("#txtPassword").val())) {
		Error = true;
	}

	// if the credentials are correct, it returns true; otherwise false
	return Error;
}

function validateEmptyInput() {
	var listInfo = [];
	var Error = false;

	listInfo.push($("#txtUsername").val());
	listInfo.push($("#txtPassword").val());

	for (var i = 0; i < listInfo.length; i++) {

		if (listInfo[i].length === 0){
			Error = true;
		}
	}

	return Error;
}

function GetToken(username, password) {

	$.post("http://localhost:49875/connect/token",
		{
			"client_id": "highschool_owner_password",
			"client_secret": "secret",
			"grant_type": "password",
			"scope": "openid profile offline_access read",
			"username": username,
			"password": password
		},
		function (data) {
			DataReponse(data);
		})
		.done(function () {
			window.location.href = '/Home/HomePage';
		})
		.fail(function (jqXHR, textStatus, error) {
			var code = jqXHR['status'];
			
			if (code === 400) {

				var message = JSON.parse(jqXHR['responseText'])['error'];

				if (message === "invalid_grant"){
					$("#errorMessage").text("Incorrect credentials");
				} else {
					$("#errorMessage").text("We have a problem with the request. Please, try in a few minutes");
				}
			} else {
				$("#errorMessage").text("We have a problem with the server. Please, try in a few minutes");
			}
		});
}

function DataReponse(data) {
	var accessToken = data['access_token'];
	var tokenType = data['token_type'];
	var username = $("#txtUsername").val();
	saveInformation(accessToken, tokenType, username);
}

function saveInformation(accessToken, tokenType, username) {
	var infoToken = [];
	infoToken.push(accessToken);
	infoToken.push(tokenType);
	infoToken.push(username);
	sessionStorage.setItem('sSToken', JSON.stringify(infoToken));
	sessionStorege.setItem('Login', JSON.stringify(true));
}








































//function Authorize(Username, Password) {

//	var settings = {
//		"url": "http://localhost:49875/connect/token",
//		"method": "POST",
//		"headers": {
//			"content-type": "application/x-www-form-urlencoded",
//		},
//		"data": {
//			"client_id": "highschool",
//			"client_secret": "secret",
//			"grant_type": "password",
//			"scope": "openid read write",
//			"username": "adrian10596@live.com",
//			"password": "password"
//		}
//	}
//	$.ajax(settings).done(function (response) {
//		console.log(response);
//	});
//}

//function AuthorizeV3(username, password) {

//	console.log("Begin Authorize...");

//	var authorizationUrl = "http://localhost:49875/connect/token";
//	var client_id = "highschool_implicit";
//	var redirect_uri = "http://localhost:52617";
//	var response_type = "token id_token";
//	var scope = "openid profile offline_access";
//	var post_logout_redirect_uri = "http://localhost:52617";
	
//	var url = authorizationUrl + "?" +
//		"client_id=" + encodeURI(client_id) + "&" +
//		"redirect_uri=" + encodeURI(redirect_uri) + "&" +
//		"response_type=" + encodeURI(response_type) + "&" +
//		"scope=" + encodeURI(scope) + "&" +
//		"post_logout_redirect_uri=" + encodeURI(post_logout_redirect_uri);

//	window.location.href = url;
//}

//window.onload = function () {
//	document.getElementById("btnSingin").addEventListener("click", AuthorizeSecondVersion, false);
//}

//var baseUrl = "http://localhost:49875/connect/";
//var tokenUrl = baseUrl + "token";
//var revokeUrl = baseUrl + "revocation";

//var client_id = "highschool";
//var client_secret = "secret";

//function getToken() {

//	var username = $("#txtUsername").val();
//	var password = $("#txtPassword").val();


//	var xhr = new XMLHttpRequest();
//	xhr.onload = function (e) {
//		console.log(xhr.status);
//		console.log(xhr.response);

//		var response_data = JSON.parse(xhr.response);
//		if (xhr.status === 200 && response_data.access_token) {
//			revokeToken(response_data.access_token);
//		}
//	}

//	xhr.open("POST", tokenUrl, true);
//	var data = {
//		username: "adrian10596@live.com",
//		password: "password",
//		grant_type: "password",
//		scope: "openid read"
//	};

//	var body = "";
//	for (var key in data) {
//		if (body.length) {
//			body += "&";
//		}
//		body += key + "=";
//		body += encodeURIComponent(data[key]);
//	}
//	xhr.setRequestHeader("Authorization", "Basic " + btoa(client_id + ":" + client_secret));
//	xhr.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
//	xhr.send(body);
//}

//function revokeToken(token) {
//	var xhr = new XMLHttpRequest();
//	xhr.onload = function (e) {
//		console.log(xhr.status);
//		console.log(xhr.response);
//	}

//	xhr.open("POST", revokeUrl);
//	var data = {
//		token: token,
//	};
//	var body = "";
//	for (var key in data) {
//		if (body.length) {
//			body += "&";
//		}
//		body += key + "=";
//		body += encodeURIComponent(data[key]);
//	}
//	xhr.setRequestHeader("Authorization", "Basic " + btoa(client_id + ":" + client_secret));
//	xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
//	xhr.send(body)
//}

//function test() {
//	var settings = {
//		// URL of your OpenID Connect server.
//		// The library uses it to access the metadata document
//		authority: 'http://localhost:49875',

//		client_id: 'highschool_implicit',

//		popup_redirect_uri: 'http://localhost:52617',
//		//silent_redirect_uri: 'http://localhost:56668/silent-renew.html',
//		post_logout_redirect_uri: 'http://localhost:52617',

//		// What you expect back from The IdP.
//		// In that case, like for all JS-based applications, an identity token
//		// and an access token
//		response_type: 'id_token token',

//		// Scopes requested during the authorisation request
//		scope: 'openid profile offline_access',

//		// Number of seconds before the token expires to trigger
//		// the `tokenExpiring` event
//		accessTokenExpiringNotificationTime: 4,

//		// Do we want to renew the access token automatically when it's
//		// about to expire?
//		automaticSilentRenew: true,

//		// Do we want to filter OIDC protocal-specific claims from the response?
//		filterProtocolClaims: true
//	};

//	// `UserManager` is the main class exposed by the library
//	var manager = new Oidc.UserManager(settings);
//	var user;

//	// You can hook a logger to the library.
//	// Conveniently, the methods exposed by the logger match
//	// the `console` object
//	Oidc.Log.logger = console;

//	// When a user logs in successfully or a token is renewed, the `userLoaded`
//	// event is fired. the `addUserLoaded` method allows to register a callback to
//	// that event
//	manager.events.addUserLoaded(function (loadedUser) {
//		user = loadedUser;
//		display('.js-user', user);
//	});

//	// Same mechanism for when the automatic renewal of a token fails
//	manager.events.addSilentRenewError(function (error) {
//		console.error('error while renewing the access token', error);
//	});

//	// When the automatic session management feature detects a change in
//	// the user session state, the `userSignedOut` event is fired.
//	manager.events.addUserSignedOut(function () {
//		alert('The user has signed out');
//	});

//	// In that case, we want the library to open a popup for the user
//	// to log in. Another possibility is to load the login form in the main window.
//	$('.js-login').on('click', function () {
//		manager
//			.signinPopup()
//			.catch(function (error) {
//				console.error('error while logging in through the popup', error);
//			});
//	});

//	// Here we want to redirect the user to the IdP logout page in the main window.
//	// We can also choose to do it in a hidden `iframe`
//	$('.js-logout').on('click', function () {
//		manager
//			.signoutRedirect()
//			.catch(function (error) {
//				console.error('error while signing out user', error);
//			});
//	});
//}