
'use strict';

var app = angular.module('moviesApp', []);

app.controller('moviesCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.app = {};

        $scope.app.loadMovies = function () {
            $http.get('/api/movies').success(function (data) {
                $scope.app.movies = data;

            }).error(function () {
                $scope.app.error = 'an unexpected error occurred';
            });
        };

        $scope.app.loadMovies();

        $scope.app.loadDirectors = function () {
            $http.get('/api/directors').success(function (data) {
                $scope.app.directors = data;

            }).error(function () {


                console.log("error");
                $scope.app.error = 'an unexpected error occurred';
            });
        };

        $scope.app.loadDirectors();

        $scope.app.searchFilter = function (obj) {
            var re = new RegExp($scope.app.searchName, 'i');
            return !$scope.app.searchName || re.test(obj.Director.Name);
        };

        $scope.app.displayAddNewMoviePanel = function () {

            // hide the edit view
            $scope.app.editedMovieId = undefined;

            // first element
            $scope.app.selectedDirector = $scope.app.directors[0].Id;

            $scope.app.showAddNewMovie = true;
        };

        $scope.app.findMovieForEditing = function (id) {
            for (var i = 0; i < $scope.app.movies.length; i++) {
                if ($scope.app.movies[i].Id == id) {

                    $scope.app.editMovieTitle = $scope.app.movies[i].Title;
                    $scope.app.editMovieYear = $scope.app.movies[i].Year;
                    $scope.app.editMoviePrice = $scope.app.movies[i].Price;
                    $scope.app.editMovieGenre = $scope.app.movies[i].Genre;
                    $scope.app.editedMovieId = id;

                    // set to match
                    $scope.app.selectedDirector = $scope.app.movies[i].Director.Id;

                    $scope.app.searchName = "";

                    $scope.app.showAddNewMovie = false;
                    break;
                }
            }
        };

        $scope.app.saveEdit = function () {
            if ($scope.app.editedMovieId != undefined) {
                var movieToEdit = {
                    Id: $scope.app.editedMovieId,
                    Title: $scope.app.editMovieTitle,
                    Year: $scope.app.editMovieYear,
                    Price: $scope.app.editMoviePrice,
                    Genre: $scope.app.editMovieGenre,
                    DirectorId: $scope.app.selectedDirector
                };
                var config = {
                    headers: { "CommandType": "UpdateMovie" }
                };
                $http.put('/api/movies/js-id'.replace("js-id", encodeURIComponent($scope.app.editedMovieId)), movieToEdit, config)
                    .success(function (data, status, headers) {
                        $scope.app.loadMovies();
                        $scope.app.clearEditingArea();
                    }).error(function () {
                        $scope.app.error = 'something\'s broken';
                    });
            }
        };

        $scope.app.clearEditingArea = function () {
            $scope.app.editedMovieId = undefined;
            $scope.app.editMovieTitle = "";
            $scope.app.editMovieYear = "";
            $scope.app.editMoviePrice = "";
            $scope.app.editMovieGenre = "";
            $scope.app.editMovieDirector = "";
        };

        $scope.app.deleteMovie = function (id) {
            var config = {
                headers: { "CommandType": "DeleteMovie" }
            };
            $http.delete('/api/movies/' + id, config).success(function (data, status, headers) {
                $scope.app.searchName = "";
                $scope.app.loadMovies();
            });
        };
    }
]);

app.controller('newMovieCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.addApp = {};
        $scope.addNewMovie = function () {

            var config = {
                headers: { "CommandType": "AddMovie" }
            };

            var newMovie = {
                Title: $scope.newMovieTitle,
                Year: $scope.newMovieYear,
                Price: $scope.newMoviePrice,
                Genre: $scope.newMovieGenre,
                DirectorId: $scope.app.selectedDirector
            };

            $http.post('/api/movies', newMovie, config).success(function (data, status, headers) {
                $scope.newMovieTitle = "";
                $scope.newMovieYear = "";
                $scope.newMoviePrice = "";
                $scope.newMovieGenre = "";

                $http.get(headers('location')).success(function (data) {
                    $scope.app.loadMovies();
                    $scope.app.searchName = "";
                });
            });

            $scope.app.showAddNewMovie = false;
        };

        $scope.cancelAddingNewMovie = function () {
            $scope.app.showAddNewMovie = false;
        };
    }]);