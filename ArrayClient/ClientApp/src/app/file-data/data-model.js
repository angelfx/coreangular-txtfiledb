"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var file_settings_1 = require("./file-settings");
//Описывает данные
var DataModel = /** @class */ (function () {
    function DataModel(data, columns, FileParameters) {
        if (FileParameters === void 0) { FileParameters = new file_settings_1.FileSettings(); }
        this.data = data;
        this.columns = columns;
        this.FileParameters = FileParameters;
    }
    return DataModel;
}());
exports.DataModel = DataModel;
//# sourceMappingURL=data-model.js.map