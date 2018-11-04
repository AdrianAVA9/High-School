function CallToApi() {

	console.log('Im inside call api')
	var headers = {};
	var user = JSON.parse(localStorage.getItem('User'));
	console.log('token: ' + user.access_token);
	if (user && user.access_token) {
		headers['Authorization'] = 'Bearer ' + user.access_token;
	}

	$.ajax({
		url: 'http://localhost:53866/api/profiles',
		method: 'GET',
		dataType: 'json',
		headers: headers
	}).then(function (data) {
		console.log('NICE.................');
		console.log(data);
		//display('.js-api-result', data);
	}).catch(function (error) {
		console.log('ERROR.................');
		console.log(data);
		//display('.js-api-result', {
		//	status: error.status,
		//	statusText: error.statusText,
		//	response: error.responseJSON
	});
}

$(document).ready(function () {
	$('.call-api').click(function () {
		CallToApi();
	});

	$(".ready").click(function () {
		getUserInformation();
	});
	$("#readyTwo").click(function () {
		sendRequestForUserInformationV3();
	});
});

function validateSession() {
	if (!JSON.parse(sessionStorage.getItem('Singin'))[0]){
		window.location.href = "/Home";
	}
}

function Logout() {
	sessionStorage.removeItem('sSToken');
	sessionStorage.setItem('Singin',JSON.stringify(false));
}

function getUserInformation() {

	sendRequestForUserInformation();
}

function sendRequestForUserInformation() {

	$.get("http://localhost:50867/api/profile",
		{
			"username":"adrian10596@live.com"
		},
		function (data) {
			showUserInformation(data);
		})
		.done(function () {
			console.log("success")
		})
		.fail(function (jqXHR, textStatus, error) {
			console.log(jqXHR);
		});
}

function showUserInformation(data) {

	var token = JSON.parse(sessionStorage.getItem('sSToken'));
	console.log(token[2]);

	var personalInformation = "Phone Number :" + data['PhoneNumber'] + "  Profile Type: " + data['ProfileType'] + 
		"  Birthday: " + data['Birthday'];

	var AboutMe = "About me: " + data['AboutMe'] + "   Email: " + data['Email'] + "  IdProfile: " + data['IdProfile'] +
		"   Registered At: " + data['RegisteredAt'];


	$("#Name").text(data['ProfileName']);
	$("#cardAboutMe").text(personalInformation);
	$("#ContactTitle").text("About me");
	$("#ContactMe").text(AboutMe);	
}






function sendRequestForUserInformationV3() {

	var token = JSON.parse(sessionStorage.getItem('sSToken'));
	var accessToken = token[0];

	var settings = {
		"async": true,
		"crossDomain": true,
		"url": "http://localhost:50867/api/profile",
		"method": "GET",
		"headers": {
			"authorization": "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IklLRUZmYUlsMVp5NS1ILXMteDh3N3plUEt1ZyIsImtpZCI6IklLRUZmYUlsMVp5NS1ILXMteDh3N3plUEt1ZyJ9.eyJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjQ5ODc1IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0OTg3NS9yZXNvdXJjZXMiLCJleHAiOjE1MjI0NjA3NzQsIm5iZiI6MTUyMjQ1NzE3NCwiY2xpZW50X2lkIjoiaGlnaHNjaG9vbCIsInNjb3BlIjpbIm9wZW5pZCIsInJlYWQiXSwic3ViIjoiYWRyaWFuMTA1OTZAbGl2ZS5jb20iLCJhdXRoX3RpbWUiOjE1MjI0NTcxNzQsImlkcCI6Imlkc3J2IiwiYW1yIjpbInBhc3N3b3JkIl19.yPvMlhTt1nbUE_v61mAXxF-9TmPasVcathUcLu5e8J4d9-vuGVKM-rjTwCLSV2VtcKTjYXPpqdedDxyBWAx1FNK0y7JVHTJZxvXj5lpCx1qb6695ReZljPAQt0_2WYSufF7BEIGTOqRdl32F3JSeCFT9No9jVDiE6MEnhvfXWYOCj4QynP4HMlJcMZkoPHKsbo6rji7fegX5bdXbCv9S0FDgsBWkshyjJydcI8V37NlfCByl4VKuRf7Kz4CfkNJiWYdRN3842kPHggSZjmc2Uvp_vD-Rv_OIkrx3VbvS1b699V6PvR4h5SuB6GVVh7v9GR2xf5woPT10LghmqoSl3w",
			"cache-control": "no-cache"
		}
	}

	$.ajax(settings)
		.done(function (response) {
			console.log(response);
		})
		.fail(function (response) {
			console.log(response);
		});
}

function sendRequestForUserInformationV2() {

	var token = JSON.parse(sessionStorage.getItem('sSToken'));
	var accessToken = token[0];

	$.get("http://localhost:50867/api/profile",
		{
			"headers": {
				"authorization": "Bearer " + accessToken
			}
		},
		function (data) {
			DataReponse(data);
		})
		.done(function () {
			console.log("success")
		})
		.fail(function (jqXHR, textStatus, error) {
			console.log(jqXHR);
		});
}