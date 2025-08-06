var app = angular.module("addressApp", []);

app.controller("AddressController", function ($scope, $http) {
    $scope.formData = {};
    $scope.results = [];
    $scope.districts = [];
    $scope.cities = [];
    $scope.searchPerformed = false;

    $scope.goToSearchPage = function () {
        window.location.href = '/Address/Search';
    };

    $scope.goToAddAddress = function () {
        window.location.href = '/Address/Index';
    };

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
        } else {
            $scope.cities = [];
        }
    };

    $scope.resetForm = function () {
        $scope.formData = {};

        if ($scope.addressForm) {
            $scope.addressForm.$setPristine();
            $scope.addressForm.$setUntouched();
        }
    }

    // Save address
    $scope.saveAddress = function (isValid) {
        if (!isValid) {
            alert("Please fix form errors before submitting.");
            return;
        }
        $http.post('/Address/SaveAddress', $scope.formData)
            .then(function (response) {
                if (response.data.success) {
                    alert("Address saved successfully!");
                    $scope.formData = {};
                    $scope.resetForm();
                } else {
                    alert("Failed to save address.");
                }
            }, function (error) {
                console.error(error);
                alert("An error occurred while saving the address.");
            });

        /*$http.post('/Address/SaveAddress', $scope.formData)
            .then(function (response) {
                alert("Address saved successfully!");
                $scope.formData = {};
                $scope.resetForm();
            }, function (error) {
                alert("Error saving address.");
            });*/
    };

    // Search addresses
    $scope.searchAddresses = function () {
        const url = '/Address/SearchAddresses?distID=' + ($scope.formData.DistID || '') + '&cityID=' + ($scope.formData.CityID || '');
        //console.log("Calling:", url); 
        $http.get(url).then(function (res) {
            $scope.results = res.data;
            $scope.searchPerformed = true;
        }, function (err) {
            //$scope.searchPerformed = true;
            console.error(err);
            alert("Error occurred during search.");
        });
    };

    $scope.deleteAddress = function (id) {
        if (confirm("Are you sure you want to delete this address?")) {
            $http.post('/Address/DeleteAddress?id=' + id).then(function (res) {
                if (res.data.success) {
                    alert("Deleted successfully!");
                    $scope.searchAddresses(); 
                } else {
                    alert("Delete failed!");
                }
            });
        }
    };

});
