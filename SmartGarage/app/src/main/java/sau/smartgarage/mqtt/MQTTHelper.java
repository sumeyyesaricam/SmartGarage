package sau.smartgarage.mqtt;

import org.eclipse.paho.client.mqttv3.IMqttDeliveryToken;
import org.eclipse.paho.client.mqttv3.MqttCallback;
import org.eclipse.paho.client.mqttv3.MqttClient;
import org.eclipse.paho.client.mqttv3.MqttConnectOptions;
import org.eclipse.paho.client.mqttv3.MqttException;
import org.eclipse.paho.client.mqttv3.MqttMessage;
import org.eclipse.paho.client.mqttv3.persist.MemoryPersistence;

/**
 * Created by Sümeyye on 1.11.2016.
 */

public class MQTTHelper {
    String topic        = "/smartgarage";
    String content      = "open";
    int qos             = 1;
    String broker       = "tcp://m13.cloudmqtt.com:11693";

    //MQTT client id to use for the device. "" will generate a client id automatically
    String clientId     = "147852369";
    MqttClient mqttClient;

    public MQTTHelper(){
        MemoryPersistence persistence = new MemoryPersistence();
        try {
             mqttClient = new MqttClient(broker, clientId, persistence);
            mqttClient.setCallback(new MqttCallback() {
                public void messageArrived(String topic, MqttMessage msg)
                        throws Exception {
                    System.out.println("Recived:" + topic);
                    System.out.println("Recived:" + new String(msg.getPayload()));
                }

                public void deliveryComplete(IMqttDeliveryToken arg0) {
                    System.out.println("Delivary complete");
                }

                public void connectionLost(Throwable arg0) {
                }
            });

            MqttConnectOptions connOpts = new MqttConnectOptions();
            connOpts.setCleanSession(true);
            connOpts.setUserName("eptdtnhw");
            connOpts.setPassword(new char[]{'5', 'T', 'Z', '2', '9', 'z', 'F','o','g','p','_','A'});
            mqttClient.connect(connOpts);
            //mqttClient.disconnect();
        } catch(MqttException me) {
            System.out.println("reason "+me.getReasonCode());
            System.out.println("msg "+me.getMessage());
            System.out.println("loc "+me.getLocalizedMessage());
            System.out.println("cause "+me.getCause());
            System.out.println("excep "+me);
            me.printStackTrace();
        }
        }

    public void Publish(String mqtMmessage){
        MqttMessage message = new MqttMessage(mqtMmessage.getBytes());
        message.setQos(qos);
        System.out.println("Publish message: " + message);
        try {
            mqttClient.subscribe(topic, qos);
            mqttClient.publish(topic, message);
        } catch (MqttException e) {
            e.printStackTrace();
        }
    }
}
