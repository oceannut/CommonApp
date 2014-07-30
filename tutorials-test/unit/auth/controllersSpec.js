'use strict';

/* jasmine specs for controllers go here */

describe('auth-controllers', function(){
  beforeEach(module('auth.controllers'));


  it('should SignInCtrl be defined', inject(function($controller) {
    var signInCtrl = $controller('SignInCtrl', { $scope: {} });
    expect(signInCtrl).toBeDefined();
  }));

  it('should SignUpCtrl be defined', inject(function($controller) {
    var signUpCtrl = $controller('SignUpCtrl', { $scope: {} });
    expect(signUpCtrl).toBeDefined();
  }));
  
});