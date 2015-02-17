(function() {
    var photoTournamentApp = angular.module('photoTournamentApp', ['ngRoute']);

    photoTournamentApp.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
        $routeProvider
            .when('/game', {
                controller: 'gameCtrl',
                templateUrl: '/app/ngviews/game.html',
            })
            .when('/winner', {
                controller: 'winnerCtrl',
                templateUrl: '/app/ngviews/winner.html',
                resolve: {
                    winningPhoto: [
                        '$http', '$q', function($http, $q) {
                            var deferred = $q.defer();
                            $http.get('/api/winner/getwinner').success(function(data) {
                                deferred.resolve(data);
                            });
                            return deferred.promise;
                        }
                    ]
                }
            })
            .when('/recent', {
                controller: 'recentCtrl',
                templateUrl: '/app/ngviews/recentWinners.html',
                resolve: {
                    data: [
                        '$http', '$q', function($http, $q) {
                            var deferred = $q.defer();
                            $http.get('/api/winner/getallwinners').success(function(data) {
                                deferred.resolve(data);
                            });
                            return deferred.promise;
                        }
                    ]
                }
            });
        $routeProvider.otherwise({ redirectTo: '/game' });

    }]);

    photoTournamentApp.controller("recentCtrl", ['$scope', '$location', 'photoService', 'data', function($scope, $location, photoService, data) {
        $scope.dataz = data;
        console.log($scope.dataz);

        $scope.getNumber = function (num) {
            var urls = [];
            for (var i = 0; i < num; i++) {
                urls[i] = '/api/winner/getallwinners?=' + i;
            }
            $scope.urls = urls;
            return urls;
        }

        $scope.update = function(page) {
            photoService.getNextTen(page).then(function(data) {
                $scope.dataz = data;
            });
        }

    }]);


    photoTournamentApp.controller("gameCtrl", ['$scope', '$location', 'photoService', function ($scope, $location, photoService) {
        $scope.loading = false;
        $scope.leftImage = "";
        $scope.rightImage = "";
        var tournament = [];
        var currentMatch = 1;
        var currentRound = 1;
        var roundsLeft = 0;
        var numRounds = 0;
        var numPlayers = 0;
        var matchCounter = 1;

        var buildStructureForWinnerData = function() {
            var tournamentArray = [];
            for (var i = 0; i < numRounds; i++) {
                tournamentArray[i] = [];
            }
            return tournamentArray;
        }

        var updateTournamentGraph = function (selection) {
            if (selection === 'left') {
                tournament[currentRound - 1][currentMatch - 1] = $scope.leftImage;
            } else {
                tournament[currentRound - 1][currentMatch - 1] = $scope.rightImage;
            }
        }

        var getNextMatchPhotos = function (roundIncremented) {
            if (roundIncremented) {
                $scope.leftImage = tournament[currentRound - 2][0];
                $scope.rightImage = tournament[currentRound - 2][1];
            } else if (currentRound > 1) {
                $scope.leftImage = tournament[currentRound - 2][currentMatch * 2];
                $scope.rightImage = tournament[currentRound - 2][currentMatch * 2 + 1];
            } else {
                $scope.leftImage = $scope.pictures[currentMatch * 2];
                $scope.rightImage = $scope.pictures[currentMatch * 2 + 1];
            }
        }

        var nextMatch = function (selection) {
            matchCounter += 1;

            updateTournamentGraph(selection);
            var roundIncremented = false;
            if (currentMatch === Math.pow(2, roundsLeft)) {
                currentRound += 1;
                roundsLeft = roundsLeft - 1;
                roundIncremented = true;
            }
            if (roundsLeft === -1) {
                $scope.loading = true;
                photoService.updateWinner(tournament[tournament.length - 1][0]).then(function() {
                    $location.path('/winner');
                });
            } else {
                getNextMatchPhotos(roundIncremented);
            }

            currentMatch = roundIncremented ? 1 : currentMatch + 1;
        }

        photoService.getPictures().then(function(data) {
            $scope.pictures = data;
            $scope.leftImage = $scope.pictures[0];
            $scope.rightImage = $scope.pictures[1];
            numPlayers = $scope.pictures.length;
            numRounds = Math.ceil(Math.log(numPlayers) / Math.log(2));
            roundsLeft = numRounds - 1;
            tournament = buildStructureForWinnerData(numPlayers);
        });

        $scope.nextLeft = function() {
            nextMatch('left');
        };

        $scope.nextRight = function() {
            nextMatch('right');
        }
    }]);



    photoTournamentApp.controller('winnerCtrl', ['$scope', '$location', '$http', 'photoService','winningPhoto', function ($scope, $location,$http, photoService,winningPhoto) {
        photoService.getWinningPhoto().then(function(url) {
            $scope.winningPhoto = url;
        });
    }]);

    photoTournamentApp.controller('mainCtrl', [function() {

    }]);

    photoTournamentApp.service('photoService', ['$q', '$http', '$location', function($q, $http, $location) {
            var getPictures = function() {
                var deferred = $q.defer();
                $http.get('/api/pictures/getphotourls' ).success(function(pictures) {
                    deferred.resolve(pictures);
                }).error(function() {
                    deferred.reject("An error occurred while retrieving user data");
                });
                return deferred.promise;
            };

            var updateWinner = function (url) {
                var postObj = { 'url' : url }
                var deferred = $q.defer();
                $http.post('/api/winner/saveWinner', postObj).success(function() {
                    deferred.resolve();
                }).error(function() {
                    deferred.reject("An error occurred while updating the winner");
                    $location.path('/error');
                });
                return deferred.promise;
            }

            var getWinningPhoto = function() {
                var deferred = $q.defer();
                $http.get('/api/winner/getWinner').success(function (data) {
                    deferred.resolve(data);
                }).error(function() {
                    deferred.reject("An error occurred while loading the winning photo");
                    $location.path('/error');
                });
                return deferred.promise;
            }

            var getNextTen = function(page) {
                var deferred = $q.defer();
                $http.get('/api/winner/getallwinners', {
                    params : {
                        page: page
                    }
                }).success(function (data) {
                    deferred.resolve(data);
                }).error(function () {
                    deferred.reject("An error occurred while loading the winning photo");
                    $location.path('/error');
                });
                return deferred.promise;
            }

            var userService = {
                getPictures: getPictures,
                updateWinner: updateWinner,
                getWinningPhoto: getWinningPhoto,
                getNextTen: getNextTen
            };

            return userService;
        }
    ]);
}());