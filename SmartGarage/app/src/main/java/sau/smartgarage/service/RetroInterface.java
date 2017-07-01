package sau.smartgarage.service;

import java.io.File;

import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.Multipart;
import retrofit2.http.POST;

/**
 * Created by Sümeyye on 29.11.2016.
 */

public interface RetroInterface {

    @FormUrlEncoded
    @POST("/smarthome/Home/OpenDoor")
    Call<String> OpenDoor(@Field("uuid") String uuid, @Field("major") String major, @Field("minor") String minor
            , @Field("name") String name, @Field("pass") String pass);
}
