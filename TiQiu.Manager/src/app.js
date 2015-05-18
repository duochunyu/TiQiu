angular.module('tiqiu', ['ui.router', 'ui.bootstrap', 'chieffancypants.loadingBar', 'ngTable'])
  .config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', 'cfpLoadingBarProvider',
    function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider, cfpLoadingBarProvider) {
      cfpLoadingBarProvider.includeSpinner = true;

      $stateProvider
        .state('login', {
          url: '^/login',
          templateUrl: 'partial/login.html',
          controller: 'LoginCtrl'
        })
        .state('book', {
          url: '^/book/:fieldId',
          templateUrl: 'partial/book.html',
          controller: 'BookCtrl'
        })
        .state('config', {
          url: '^/config/:fieldId',
          templateUrl: 'partial/config.html',
          controller: 'ConfigCtrl'
        })
        .state('order', {
          url: '^/order/:fieldId',
          templateUrl: 'partial/order.html',
          controller: 'OrderCtrl'
        })
        .state('password', {
            url: '^/password/:fieldId',
            templateUrl: 'partial/password.html',
           controller: 'PasswordCtrl'
      });
      $urlRouterProvider.otherwise('login');
    }
  ])
  .run(['$rootScope', '$state', '$stateParams', 'Auth', '$notification', 'Message',
    function ($rootScope, $state, $stateParams, Auth, $notification, Message) {

      // It's very handy to add references to $state and $stateParams to the $rootScope
      // so that you can access them from any scope within your applications.For example,
      // <li ui-sref-active="active }"> will set the <li> // to active whenever
      // 'contacts.list' or one of its decendents is active.
      $rootScope.$state = $state;
      $rootScope.$stateParams = $stateParams;

      $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
        if (!Auth.isLoggedIn() && toState.name !== 'login') {
          event.preventDefault();
          $state.go('login');
        }
      });

      $rootScope.$on('newMessageComing', function (data) {
        $notification.success('提醒', '您有新的预定<a>点击查看</a>');
      });

      // setInterval(function() {
      //     Message.getMessage()
      //         .then(function(data) {
      //             if (data.length) {
      //                 data.forEach(function(msg) {
      //                     $notification.success('提醒', '您有新的预定<a href="#/book/' + msg.FieldId + '_' + msg.FieldItemId + '">点击查看</a>');
      //                 });
      //             }
      //         });
      // }, 1000 * 30);
    }
  ]);