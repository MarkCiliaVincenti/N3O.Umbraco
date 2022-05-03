export declare class ExportClient {
    private http;
    private baseUrl;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined;
    constructor(baseUrl?: string, http?: {
        fetch(url: RequestInfo, init?: RequestInit): Promise<Response>;
    });
    getExportableProperties(contentId: string, contentType: string): Promise<void>;
    protected processGetExportableProperties(response: Response): Promise<void>;
    createExport(contentId: string, contentType: string, req: ExportReq): Promise<void>;
    protected processCreateExport(response: Response): Promise<void>;
}
export interface ProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
}
export interface ExportReq {
    properties?: string[] | undefined;
    includeUnpublished?: boolean | undefined;
    format?: WorkbookFormat | undefined;
}
/** One of 'csv', 'excel' */
export declare enum WorkbookFormat {
    Csv = "csv",
    Excel = "excel"
}
export declare class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: {
        [key: string]: any;
    };
    result: any;
    constructor(message: string, status: number, response: string, headers: {
        [key: string]: any;
    }, result: any);
    protected isApiException: boolean;
    static isApiException(obj: any): obj is ApiException;
}
//# sourceMappingURL=index.d.ts.map