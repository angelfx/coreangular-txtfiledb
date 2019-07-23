"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var FileSettings = /** @class */ (function () {
    function FileSettings(fileName, filePath, firstRowIsColumns, colsNumber, separator) {
        if (firstRowIsColumns === void 0) { firstRowIsColumns = false; }
        this.fileName = fileName;
        this.filePath = filePath;
        this.firstRowIsColumns = firstRowIsColumns;
        this.colsNumber = colsNumber;
        this.separator = separator;
    }
    return FileSettings;
}());
exports.FileSettings = FileSettings;
//# sourceMappingURL=file-settings.js.map