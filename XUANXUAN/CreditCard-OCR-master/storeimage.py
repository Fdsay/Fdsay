import os
import mysql.connector

# 连接到MySQL数据库
conn = mysql.connector.connect(
    host="localhost",
    user="root",
    password="123456",
    database="card"
)
cursor = conn.cursor()
# 定义创建数据表的SQL语句
create_table_query = """
CREATE TABLE IF NOT EXISTS cardimage (
    id INT PRIMARY KEY,
    path VARCHAR(255),
    name VARCHAR(255)
)
"""
# 创建数据表
cursor.execute(create_table_query)
# 定义插入数据的SQL语句
insert_query = "INSERT INTO cardimage (id, path, name) VALUES (%s, %s, %s)"

# 文件夹路径
folder_path = r"D:\code\CreditCard-OCR-master\picture"

# 获取文件夹中的所有图片文件
image_files = [f for f in os.listdir(folder_path) if os.path.isfile(os.path.join(folder_path, f)) and f.endswith(('.jpg', '.jpeg', '.png', '.gif'))]

# 遍历图片文件并将其插入到数据库表中
for index, image_file in enumerate(image_files, start=1):
    # 处理文件名编码问题
    image_name = image_file.encode('latin1').decode('gbk')
    image_path = os.path.join(folder_path, image_file).encode('latin1').decode('gbk')
    cursor.execute(insert_query, (index, image_path, image_name))


# 提交事务并关闭连接
conn.commit()
conn.close()
