export declare class DonationsClient {
    private http;
    private baseUrl;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined;
    constructor(baseUrl?: string, http?: {
        fetch(url: RequestInfo, init?: RequestInit): Promise<Response>;
    });
    getForm(id: string): Promise<DonationFormRes>;
    protected processGetForm(response: Response): Promise<DonationFormRes>;
}
export interface DonationFormRes {
    title?: string | undefined;
    options?: DonationOptionRes[] | undefined;
}
export interface DonationOptionRes {
    type?: AllocationType | undefined;
    dimension1?: FixedOrDefaultFundDimensionOptionRes | undefined;
    dimension2?: FixedOrDefaultFundDimensionOptionRes | undefined;
    dimension3?: FixedOrDefaultFundDimensionOptionRes | undefined;
    dimension4?: FixedOrDefaultFundDimensionOptionRes | undefined;
    hideQuantity?: boolean;
    hideDonation?: boolean;
    hideRegularGiving?: boolean;
    fund?: FundDonationOptionRes | undefined;
    sponsorship?: SponsorshipDonationOptionRes | undefined;
}
/** One of 'fund', 'sponsorship' */
export declare enum AllocationType {
    Fund = "fund",
    Sponsorship = "sponsorship"
}
export interface FixedOrDefaultFundDimensionOptionRes {
    fixed?: FundDimensionOptionRes | undefined;
    default?: FundDimensionOptionRes | undefined;
}
export interface FundDimensionOptionRes {
    name?: string | undefined;
    id?: string | undefined;
    isUnrestricted?: boolean;
}
export interface FundDonationOptionRes {
    donationItem?: string | undefined;
    donationPriceHandles?: PriceHandleRes[] | undefined;
    regularGivingPriceHandles?: PriceHandleRes[] | undefined;
}
export interface IPublishedContent {
    id?: number;
    name?: string | undefined;
    urlSegment?: string | undefined;
    sortOrder?: number;
    level?: number;
    path?: string | undefined;
    templateId?: number | undefined;
    creatorId?: number;
    createDate?: Date;
    writerId?: number;
    updateDate?: Date;
    cultures?: {
        [key: string]: PublishedCultureInfo;
    } | undefined;
    itemType?: PublishedItemType;
    parent?: IPublishedContent | undefined;
    children?: IPublishedContent[] | undefined;
    childrenForAllCultures?: IPublishedContent[] | undefined;
}
export interface PublishedCultureInfo {
    culture?: string | undefined;
    name?: string | undefined;
    urlSegment?: string | undefined;
    date?: Date;
}
export declare enum PublishedItemType {
    Unknown = 0,
    Element = 1,
    Content = 2,
    Media = 3,
    Member = 4
}
/** One of 'donation', 'regularGiving' */
export declare enum GivingType {
    Donation = "donation",
    RegularGiving = "regularGiving"
}
export interface PriceContent {
    content?: IPublishedContent | undefined;
    amount?: number;
    locked?: boolean;
}
export interface PricingRuleContent {
    content?: IPublishedContent | undefined;
    price?: PriceContent | undefined;
    dimension1Options?: string[] | undefined;
    dimension2Options?: string[] | undefined;
    dimension3Options?: string[] | undefined;
    dimension4Options?: string[] | undefined;
}
export interface PriceHandleRes {
    amount?: MoneyRes | undefined;
    description?: string | undefined;
}
export interface MoneyRes {
    amount?: number;
    currency?: string | undefined;
    text?: string | undefined;
}
export interface SponsorshipDonationOptionRes {
    scheme?: string | undefined;
}
export interface ProblemDetails {
    type?: string | undefined;
    title?: string | undefined;
    status?: number | undefined;
    detail?: string | undefined;
    instance?: string | undefined;
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