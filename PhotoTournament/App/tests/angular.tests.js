describe('ControllerTests', function () {
    var createController;
    var scope;
    var location;
    var photoService;

    beforeEach(module('photoTournamentApp'));

    beforeEach(inject(function($controller, $rootScope) {
        scope = $rootScope.$new();
        $controller('gameCtrl', { $scope: scope });

    }));

    it('should have variable loading = false', function() {
        expect(scope.loading).toBe(false);
    });

    it('left should not be null', function() {
        expect(scope.leftImage).not.toBeNull();
    });

    it('right should not be null', function() {
        expect(scope.rightImage).not.toBeNull();
    });
});  