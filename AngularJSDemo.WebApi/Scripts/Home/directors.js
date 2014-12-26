
'use strict';

var app = angular.module('directorsApp', []);

app.controller('directorsCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.app = {};

        $scope.app.loadDirectors = function () {
            $http.get('/api/directors').success(function (data) {
                $scope.app.directors = data;
            }).error(function () {
                $scope.app.error = 'an unexpected error occurred';
            });
        };

        $scope.app.loadDirectors();

        $scope.app.displayAddNewDirectorPanel = function () {
            $scope.app.showAddNewDirector = true;
        };

        $scope.app.findDirectorForEditing = function (id) {
            for (var i = 0; i < $scope.app.directors.length; i++) {
                console.log('first: i = ' + i + ', & id = ' + id);
                if ($scope.app.directors[i].Id == id) {
                    $scope.app.editDirectorName = $scope.app.directors[i].Name;
                    $scope.app.editedDirectorId = id;
                    $scope.app.showAddNewDirector = false;
                    break;
                }
            }
        };

        $scope.app.saveEdit = function () {
            if ($scope.app.editedDirectorId != undefined) {
                var directorToEdit = {
                    Id: $scope.app.editedDirectorId,
                    Name: $scope.app.editDirectorName
                };
                var config = {
                    headers: { "CommandType": "UpdateDirector" }
                };
                $http.put('/api/directors/js-id'.replace("js-id", encodeURIComponent($scope.app.editedDirectorId)), directorToEdit, config)
                    .success(function (data, status, headers) {
                        $scope.app.loadDirectors();
                        $scope.app.clearEditingArea();
                    }).error(function () {
                        $scope.app.error = 'something\'s broken';
                    });
            }
        };

        $scope.app.clearEditingArea = function () {
            $scope.app.editedDirectorId = undefined;
            $scope.app.editDirectorName = "";
        };

        $scope.app.deleteDirector = function (id) {
            var config = {
                headers: { "CommandType": "DeleteDirector" }
            };
            $http.delete('/api/directors/' + id, config).success(function (data, status, headers) {
                $scope.app.loadDirectors();
            });
        };
    }
]);

app.controller('newDirectorCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.addApp = {};
        $scope.addNewDirector = function () {

            var config = {
                headers: { "CommandType": "AddDirector" }
            };
            var newDirector = {
                Name: $scope.newDirectorName,
            };
            $http.post('/api/directors', newDirector, config).success(function (data, status, headers) {
                $scope.newDirectorName = "";

                $http.get(headers('location')).success(function (data) {
                    $scope.app.loadDirectors();
                });
            });

            $scope.app.showAddNewDirector = false;
        };

        $scope.cancelAddingNewDirector = function () {
            $scope.app.showAddNewDirector = false;
        };
    }]);