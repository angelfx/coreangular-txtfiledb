export class FileSettings {
  constructor(
    public fileName?: string,
    public filePath?: string,
    public firstRowIsColumns: boolean = false,
    public colsNumber?: number,
    public separator?: string
  ) { }
}
