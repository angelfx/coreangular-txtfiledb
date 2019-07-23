"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var file_settings_1 = require("./file-settings");
//Описывает строку
var RowModel = /** @class */ (function () {
    function RowModel(indexRow, row, FileParameters) {
        if (FileParameters === void 0) { FileParameters = new file_settings_1.FileSettings(); }
        this.indexRow = indexRow;
        this.row = row;
        this.FileParameters = FileParameters;
    }
    return RowModel;
}());
exports.RowModel = RowModel;
//# sourceMappingURL=row-model.js.map