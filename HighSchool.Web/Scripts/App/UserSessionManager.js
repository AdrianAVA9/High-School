//$(document).ready(function () {

//    var settings = {
//        authority: 'http://localhost:49875',
//        client_id: 'highschool_implicit',
//        popup_redirect_uri: 'http://localhost:56622/popup',
//        response_type: 'token id_token',
//        scope: 'openid profile read',
//        filterProtocolClaims: true
//    };

//    var manager = new Oidc.UserManager(settings);
//    var user;

//    manager.events.addUserLoaded(function (loadedUser) {
//        user = loadedUser;
//        display('.js-user', user);
//        localStorage.setItem('User',JSON.stringify(user));
//    });

//    $('.js-login').click(function () {
//        console.log("--In in of identity server")
//        manager
//            .signinPopup()
//            .then(function (userInSession) {
//                localStorage.setItem('User', JSON.stringify(userInSession)); 
//            })
//            .catch(function (error) { 
//                console.log('error while loggin in through the popup', error);
//            });
//    });
//});

//function display(selector, data) {
//    if (data && typeof data === 'string') {
//        data = JSON.parse(data);
//    }
//    if (data) {
//        data = JSON.stringify(data, null, 2);
//    }

//    $(selector).text(data);
//}