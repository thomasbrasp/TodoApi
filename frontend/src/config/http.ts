import Axios, {type AxiosRequestConfig, type AxiosInstance} from 'axios';

export const AXIOS_INSTANCE = Axios.create({
    baseURL: 'http://localhost:5062/', // use your own URL or environment variable
});

const customInstance = <T>(axiosInstance: AxiosInstance, config: AxiosRequestConfig, options?: AxiosRequestConfig): Promise<T> =>
    axiosInstance({
        ...config,
        ...options,
    }).then(({ data }) => data as T);

export const todoInstance = <T>(config: AxiosRequestConfig, options?: AxiosRequestConfig): Promise<T> => {
    return customInstance(AXIOS_INSTANCE, config, options);
};