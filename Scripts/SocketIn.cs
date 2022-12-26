using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Net;
using System.Globalization;

public class SocketIn : MonoBehaviour
{
	#region private members 	
	private TcpClient socketConnection;
	private Thread clientReceiveThread;
	#endregion
	// Use this for initialization 	
	public float joint1;
	public float joint2;
	public float joint3;
	public float joint4;
	public float joint5;
	public float joint6;
	public float joint7;
	public float pos;
	public float sinus; 
	void Start()
	{
		ConnectToTcpServer(); 
	}
	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	SendMessage(modeToSend.mode.ToString());
		//}
	}
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer()
	{
		try
		{
			clientReceiveThread = new Thread(new ThreadStart(ListenForData));
			clientReceiveThread.IsBackground = true;
			clientReceiveThread.Start();
		}
		catch (Exception e)
		{
			Debug.Log("On client connect exception " + e);
		}
	}
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>    
	/// 

	private void ListenForData()
	{
		try
		{
			socketConnection = new TcpClient("192.168.1.100", 8005);
			Byte[] bytes = new Byte[1024];
			while (true)
			{
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream())
				{
					int length = 0;

					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
					{
						var incommingData = new byte[length];
						Array.Copy(bytes, 0, incommingData, 0, length);
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData);

						serverMessage = serverMessage.Replace("[", string.Empty);

						String[] lines = serverMessage.Split(']');

						foreach (String line in lines)
                        {
							if (line != "")
                            {
								string[] joints = line.Split(',');

								if (joints.Length > 8)
                                {

									joint1 = float.Parse(joints[0], CultureInfo.InvariantCulture);
									joint2 = float.Parse(joints[1], CultureInfo.InvariantCulture);
									joint3 = float.Parse(joints[2], CultureInfo.InvariantCulture);
									joint4 = float.Parse(joints[3], CultureInfo.InvariantCulture);
									joint5 = float.Parse(joints[4], CultureInfo.InvariantCulture);
									joint6 = float.Parse(joints[5], CultureInfo.InvariantCulture);
									joint7 = float.Parse(joints[6], CultureInfo.InvariantCulture);
									pos = float.Parse(joints[7], CultureInfo.InvariantCulture);
									sinus = float.Parse(joints[8], CultureInfo.InvariantCulture);
                                    //Debug.Log(joints[0]);
                                }

                            }



						}



					}
				}
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	public void SendMessage(String messageGet)
	{
		if (socketConnection == null)
		{
			return;
		}
		try
		{
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream();
			if (stream.CanWrite)
			{
				string clientMessage = messageGet;
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
				Debug.Log("Client sent his message - should be received by server");
			}
		}
		catch (SocketException socketException)
		{
			Debug.Log("Socket exception: " + socketException);
		}
	}
}