# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
"""Client and server classes corresponding to protobuf-defined services."""
import grpc

import cannyedge_pb2 as cannyedge__pb2


class CannyEdgeDetectorStub(object):
    """Missing associated documentation comment in .proto file."""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.DetectEdges = channel.stream_stream(
                '/CannyEdgeDetector/DetectEdges',
                request_serializer=cannyedge__pb2.DetectEdgesRequest.SerializeToString,
                response_deserializer=cannyedge__pb2.DetectEdgesResponse.FromString,
                )


class CannyEdgeDetectorServicer(object):
    """Missing associated documentation comment in .proto file."""

    def DetectEdges(self, request_iterator, context):
        """Missing associated documentation comment in .proto file."""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_CannyEdgeDetectorServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'DetectEdges': grpc.stream_stream_rpc_method_handler(
                    servicer.DetectEdges,
                    request_deserializer=cannyedge__pb2.DetectEdgesRequest.FromString,
                    response_serializer=cannyedge__pb2.DetectEdgesResponse.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'CannyEdgeDetector', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class CannyEdgeDetector(object):
    """Missing associated documentation comment in .proto file."""

    @staticmethod
    def DetectEdges(request_iterator,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            insecure=False,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.stream_stream(request_iterator, target, '/CannyEdgeDetector/DetectEdges',
            cannyedge__pb2.DetectEdgesRequest.SerializeToString,
            cannyedge__pb2.DetectEdgesResponse.FromString,
            options, channel_credentials,
            insecure, call_credentials, compression, wait_for_ready, timeout, metadata)