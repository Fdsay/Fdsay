import os
import mysql.connector

# ���ӵ�MySQL���ݿ�
conn = mysql.connector.connect(
    host="localhost",
    user="root",
    password="123456",
    database="card"
)
cursor = conn.cursor()
# ���崴�����ݱ��SQL���
create_table_query = """
CREATE TABLE IF NOT EXISTS cardimage (
    id INT PRIMARY KEY,
    path VARCHAR(255),
    name VARCHAR(255)
)
"""
# �������ݱ�
cursor.execute(create_table_query)
# ����������ݵ�SQL���
insert_query = "INSERT INTO cardimage (id, path, name) VALUES (%s, %s, %s)"

# �ļ���·��
folder_path = r"D:\code\CreditCard-OCR-master\picture"

# ��ȡ�ļ����е�����ͼƬ�ļ�
image_files = [f for f in os.listdir(folder_path) if os.path.isfile(os.path.join(folder_path, f)) and f.endswith(('.jpg', '.jpeg', '.png', '.gif'))]

# ����ͼƬ�ļ���������뵽���ݿ����
for index, image_file in enumerate(image_files, start=1):
    # �����ļ�����������
    image_name = image_file.encode('latin1').decode('gbk')
    image_path = os.path.join(folder_path, image_file).encode('latin1').decode('gbk')
    cursor.execute(insert_query, (index, image_path, image_name))


# �ύ���񲢹ر�����
conn.commit()
conn.close()
