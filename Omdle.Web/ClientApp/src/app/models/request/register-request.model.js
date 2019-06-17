"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var RegisterRequest = /** @class */ (function() {
  function RegisterRequest(Pesel, Password, FirstName, LastName) {

    this.Pesel = Pesel;
    this.Password = Password;
    this.Name = FirstName;
    this.Surname = LastName;
  }

  return RegisterRequest;
}());
exports.RegisterRequest = RegisterRequest;
//# sourceMappingURL=register-request.model.js.map
