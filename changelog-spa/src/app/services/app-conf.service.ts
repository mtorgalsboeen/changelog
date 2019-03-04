import conf from 'src/environments/conf.json' 

/**
 * Provides acess to common configuration values. The values are acesses in this fashion: AppConfService.config.<configOption>
 */
export class AppConfService {

  static config:Config = null;

  loadConfig(){
   //Converting to JSSON
   AppConfService.config = Object.assign(new Config(),conf);
  }

}
/**Used for mappig the JSOn-file to a object. The name of the fields needs to match the json(case sensitive) */
export class Config{
  public httpRequest:httpRequest;
  public uiMessages:uiMessages;
}

export class httpRequest{
  public apiUrl:string;
  public minLoadingMillis:number;
  public delayBetweenEachRetryMillis:number;
  public numRetries:number;
}

export class uiMessages{
  public httpRequestFailed:string;
  public httpLoading:string;
  public noNetwork:string;
}
