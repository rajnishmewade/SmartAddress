var app = angular.module("addressApp", []);

app.controller("AddressController", function ($scope, $http) {
    $scope.formData = {};

    //$scope.showForm = false;
    //$scope.imageUrl = "https://images.unsplash.com/photo-1449824913935-59a10b8d2000?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D";

    //$scope.imageUrl = "https://source.unsplash.com/1600x400/?city,address&sig=" + Math.random();

    $scope.districts = [];
    $scope.cities = [];

    // serach ke liye 
    $scope.showSearchPage = false;

    $scope.goToSearchPage = function () {
        $scope.showSearchPage = true;
    };

    $scope.searchResults = [];

    // Load districts on page load
    $http.get('/Address/GetDistricts').then(function (res) {
        $scope.districts = res.data;
    });

    // Load cities when district changes
    $scope.loadCities = function () {
        if ($scope.formData.DistID) {
            $http.get('/Address/GetCitiesByDistrict/' + $scope.formData.DistID).then(function (res) {
                $scope.cities = res.data;
            });
        }
    };

    // Save address
    $scope.saveAddress = function () {
        $http.post('/Address/SaveAddress', $scope.formData)
            .then(function (response) {
                alert("Address saved successfully!");
                $scope.formData = {};
            }, function (error) {
                alert("Error saving address.");
            });
    };

    /*$scope.searchAddresses = function () {
        let distID = $scope.searchDistID;
        let cityID = $scope.searchCityID;

        let url = '/api/Address/Search?distID=' + (distID || '') + '&cityID=' + (cityID || '');

        $http.get(url)
            .then(function (response) {
                $scope.searchResults = response.data;
            })
            .catch(function (error) {
                alert("Search failed: " + error.data.message || "Server error");
            });
    };*/

});
